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

    [SerializeField]
    GameManager gameManager;

    public enum Direction {left, right};
    Direction facingDirection;

    bool changeDirection = false;

    float yRotation = 90f;
    float yRotationLerpValue = 0f;
    Vector3 playerTransform;
    bool turning = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //Direction facingDirection;
        facingDirection = Direction.right;
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

            if (changeDirection == true)
            {
                turning = true;
                changeDirection = false;
            }
            if(turning == true) {
                yRotationLerpValue += Time.deltaTime * 4;
                if (facingDirection == Direction.right)
                {
                    Debug.Log("right to left");
                    playerTransform = Vector3.Lerp(new Vector3(0f, 270f, 0f), new Vector3(0f, 90f, 0f), yRotationLerpValue);
                }
                if (facingDirection == Direction.left)
                {
                    Debug.Log("left to rigght");
                    playerTransform = Vector3.Lerp(new Vector3(0f, 90f, 0f), new Vector3(0f, 270f, 0f), yRotationLerpValue);
                }
                Debug.Log(yRotationLerpValue);

                if (yRotationLerpValue >= 1)
                {
                    turning = false;
                    yRotationLerpValue = 0f;
                }


                transform.rotation = Quaternion.Euler(playerTransform);//Quaternion.Euler(0, yRotation, 0);


                //Vector3 interpolatedPosition = Vector3.Lerp(Vector3.up, Vector3.forward, interpolationRatio);

            }



            if (Input.GetKeyDown(KeyCode.F))
            {
                dropLight();
            }


        }
    }

    void Jump()
    {
        pressedJumpTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //animator.Trigger(Jump);
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

    public void LRMove()
    {
        
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (rb.velocity.x > 0.1f || rb.velocity.x < -0.1f)
            animator.SetBool("Moving", true);
        else
            animator.SetBool("Moving", false);

        if (moveInput > 0)
        {
            if (facingDirection == Direction.left)
            {
                changeDirection = true;
            }
            facingDirection = Direction.right;

        }
        else if (moveInput < 0)
        {
            if (facingDirection == Direction.right)
            {
                changeDirection = true;
            }
            facingDirection = Direction.left;

        }
    }




    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
            gameManager.PlayerHit();
    }


    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }



    void dropLight()
    {
        //totalLight++;
        currentLight = Instantiate(deadLight, new Vector3(transform.position.x, transform.position.y, -5f), Quaternion.identity);
    }


}
