using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float delta = 1.5f;  // Amount to move left and right from the start point
    public float speed = 2.0f;
    private Vector2 startPos;
    
    


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = startPos;
        v.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;

        if (Input.GetKey("escape"))
            Application.Quit();

        if (delta * Mathf.Sin(Time.time * speed) > 1.48)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = 2.75f;
            transform.localScale = theScale;
        }

        if (delta * Mathf.Sin(Time.time * speed) < -1.48)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -2.75f;
            transform.localScale = theScale;
        }


    }

    
}
