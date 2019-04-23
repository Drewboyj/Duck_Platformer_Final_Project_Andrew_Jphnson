using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public float checkRadiusQuack;
    private int extraJumps;
    public int extraJumpsValue;
    public LayerMask whatIsGround;
    private bool facingRight;
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpforce;
    private int count;
    public Text countText;
    public Text winText;
    public Text livesText;
    private int lives;
    public AudioSource winSource;
    private float pressedTime;

    private bool bite;
    //private bool quack;
    public Vector2 velocity;
    private bool walk, walk_left, walk_right;
    bool jump = false;
    

    void Start()
    {
        extraJumps = extraJumpsValue;
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText();
        winText.text = "";
        lives = 3;
        SetlivesText();
               
        facingRight = true;
        
    }


    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(moveHorizontal));
                
        CheckPlayerInput();
        UpdatePlayerPosition();

        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;            
        }

        

        
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps >0)
        {
            rb2d.velocity = new Vector2(0, 0);
            rb2d.AddForce(new Vector2(velocity.x, jumpforce));                    
            extraJumps--;            
        }

        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb2d.velocity = new Vector2(0, 0);
            rb2d.AddForce(new Vector2(velocity.x, jumpforce));            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Physics2D.gravity = new Vector2(0, -3.5f);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Physics2D.gravity = new Vector2(0, -25f);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = false;
            anim.SetBool("IsFlying", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            anim.SetBool("IsFlying", true);
            pressedTime = Time.time;
        }

        if (isGrounded && Time.time - pressedTime > .5f)
        {
            jump = false;
            anim.SetBool("IsFlying", false);
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse1))
        {
           // bite = true;
        }

        if (bite)
        {
           // anim.SetTrigger("Bite");
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            //quack = true;
        }

        //if (quack)
        {
            //anim.SetTrigger("Quack");
        }
        //quack = false;
        bite = false;


    }

    

    void CheckPlayerInput()
    {
        bool input_left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool input_right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        walk = input_left || input_right;
        walk_left = input_left && !input_right;
        walk_right = !input_left && input_right;
    }

    void UpdatePlayerPosition()
    {
        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Bite"))
        {
            if (!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Quack"))
            {
                Vector3 pos = transform.localPosition;

                if (walk)
                {
                    if (walk_left)
                    {
                        pos.x -= velocity.x * Time.deltaTime;

                    }

                    if (walk_right)
                    {
                        pos.x += velocity.x * Time.deltaTime;
                    }
                }

                transform.localPosition = pos;
            }
            

        }

       
    }


    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        Flip(moveHorizontal);      

    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

            if (count == 4)
            {
                transform.position = new Vector3(48.5f, .0f, .0f);
                Camera.main.transform.position = new Vector3(48.5f, .0f, -10.0f);
                lives = 3;
                SetlivesText();
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetlivesText();
        }

        
    }



    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winText.text = "You Win!";

            winSource.Play();
            transform.position = new Vector3(-55.0f, -1.0f, .0f);


        }


    }

    void SetlivesText()
    {
        livesText.text = "lives: " + lives.ToString();
        bool v = lives <= 0;
        if (v)
        {
            winText.text = "You Lose.";
            if (gameObject.tag == "Player")
            {
                Destroy(obj: gameObject);
            }

        }

    }

    private void Flip(float moveHorizontal)
    {
        if (moveHorizontal > 0 && facingRight || moveHorizontal < 0 && !facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    
}






