using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuack : MonoBehaviour
{
    public Animator anim;
    private float timeBtwQuack;
    public float startTimeBtwQuack;
    public AudioSource quackSource;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;
    private bool quack;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    void Update()
    {
        if (isGrounded == true)
        {
            if (timeBtwQuack <= 0)
            {
                if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Mouse0))
                {

                    quack = true;

                    if (quack)
                    {
                        anim.SetTrigger("Quack");
                    }

                    quackSource.Play();

                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    }
                    quack = false;
                }

                timeBtwQuack = startTimeBtwQuack;
            }
            else
            {
                timeBtwQuack -= Time.deltaTime;
            }
        }
        
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
