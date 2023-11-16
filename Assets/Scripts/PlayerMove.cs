using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;

    Animator animator;

    [SerializeField]
    float pressedJumpTimer;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float speed;
    float moveInput;

    bool isGrounded;

    [SerializeField]
    float jumpingTimer = 0f;

    public GameObject deadLight;
    public Vector3 levelRespawnPoint;

    int totalLights = 0;
    //[SerializeField]
    //GameObject[] lightList;
    public GameObject currentLight;
    public GameObject tempLight;
    //ArrayList lightList = new ArrayList();
    GameObject[] lightList = new GameObject[2];



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
        
    }


    void FixedUpdate()
    {
        if (jumpingTimer <= 0f && isGrounded == false)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + -1f);
    }



    void Update()
    {
        if (!PauseMenu.paused)
        {
            //Input
            Jump();
            LRMove();


            if (Input.GetKeyDown(KeyCode.F))
            {
                dropLight();
            }

            if (transform.position.y <= -8 || transform.position.y >= 8)
                playerHit();
        }
    }

    void Jump()
    {
        pressedJumpTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressedJumpTimer = 0.15f;

        }
        if ((pressedJumpTimer > 0) && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            //amount of time holding jump is allowed(larger number = can float in air longer)
            jumpingTimer = 0.3f;
            //Debug.Log("hit");

        }

        if (Input.GetKey(KeyCode.Space) && jumpingTimer > 0f)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpingTimer -= Time.deltaTime;
        }
        else
            jumpingTimer = 0f;

    }

    void LRMove()
    {
        
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (rb.velocity.x > 0.1f || rb.velocity.x < -0.1f)
            animator.SetBool("Moving", true);
        else
            animator.SetBool("Moving", false);
    }




    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = true;
        if (other.gameObject.CompareTag("Obstacle"))
            playerHit();

    }

    private void OnCollisionExit(Collision other)
    {   
        if(other.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    private void playerHit()
    {

        transform.position = levelRespawnPoint;
        
    }

    void dropLight()
    {
        //totalLight++;
        currentLight = Instantiate(deadLight, transform.position, Quaternion.identity);
    }


}
