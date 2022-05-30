using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    Rigidbody2D  rbody;
    Animator anim;

    bool  walking , jumped ,jumping, grounded = false;
    public float speed = 10f, height = 40f;
    float jumpTime,walkTime;
    int moveState;
    
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        State();
    }

    private void FixedUpdate()
    {
    if (!Input.GetKey(KeyCode.RightArrow) || !(Input.GetKey(KeyCode.UpArrow))
        || Input.GetKey(KeyCode.LeftArrow))
        {
            moveState = 0;
        }
    if (Input.GetKey(KeyCode.RightArrow))
        moveState = 1;
     if (Input.GetKey(KeyCode.LeftArrow))
        moveState = 2;

        Jump();
    }
    void Move(Vector3 dir)
    {
        walking= true;
        speed= Mathf.Clamp(speed,speed,10f);
        walkTime += Time.deltaTime;
        
        transform.Translate(dir * speed * Time.fixedDeltaTime);
        if(walkTime < 2f && walking)
        {
            speed += .020f;
        }

        else if(walkTime> 3f)
        {
            speed = 5f;
        }
    }
    void Jump (){
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (grounded)
            {
                rbody.velocity = new Vector2(rbody.velocity.x,height);
            }
        }
        if (jumping && jumped)
        {
            rbody.gravityScale -= (0.1f*Time.fixedDeltaTime);
        }
        if (jumpTime > 1f)
            jumping = false;
        if (!jumping && jumped)
            rbody.gravityScale += .2f;
    }
    private void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false);

            GameController.instance.GameOver();

            Time.timeScale = 0;
        }
    }

    private void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            jumped=false;
            jumping=false;

            anim.SetBool("Jumps",false);
            jumpTime = 0;
            rbody.gravityScale =5;
        }
    } 

    private void  OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false);

            GameController.instance.GameOver();

            Time.timeScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
     {
        if (collision.gameObject.tag == "Ground")
        {
            grounded =false;
        }
    }


    void State()
    {
        switch (moveState)
        {
            case 1:
            anim.SetBool("Runs",true);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),
                transform.localScale.y,transform.localScale.z);
            Move(Vector3.right);
            break;
            case 2:
            anim.SetBool("Runs",true);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),
                transform.localScale.y,transform.localScale.z);
            Move(Vector3.left);
            break;
            default:
                walking = false;
                walkTime = 0;
                speed=40f;
                anim.SetBool("Runs", false);
                break;
        }
        //88888888888
        if (Input.GetKey(KeyCode.UpArrow))
        {
            jumped = true; jumping=true;
            jumpTime += Time.fixedDeltaTime;
        }
        else jumping = false;
    }

}
