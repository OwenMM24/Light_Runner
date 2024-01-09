using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;

    Animator animator;

    [SerializeField] LayerMask jumpableGround;

    [SerializeField]
    float pressedJumpTimer;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float speed;
    float moveInput;

    bool grounded = false;

    [SerializeField]
    float jumpingTimer = 0f;

    public Light deadLight;
    public Vector3 levelRespawnPoint;

    private CapsuleCollider coll;

    int totalLights = 0;
    //[SerializeField]
    //GameObject[] lightList;
    public Light currentLight;
    public GameObject tempLight;
    //ArrayList lightList = new ArrayList();
    //GameObject[] lightList = new GameObject[2];

    [SerializeField]
    GameManager gameManager;

    public enum Direction {left, right};
    Direction facingDirection;

    bool changeDirection = false;

    float yRotation = 90f;
    float yRotationLerpValue = 0f;
    Vector3 playerTransform;
    bool turning = false;

    [SerializeField]
    GameObject followPlayerLight;

    float blueLightBuff = 1f;

    bool droppedLight = false;
    float dropLightTime = 0f;
    bool firstTimeThrough = true;

    Light light1, light2, light3;


    //Light[] lightList = new Light[2];
    int lightListIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //Direction facingDirection;
        facingDirection = Direction.right;
    }


    void FixedUpdate()
    {
        if (jumpingTimer <= 0f && IsGrounded() == false)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + -1f);

        if(droppedLight == true)
        {

            dropLightTime += Time.deltaTime * 2.5f;
            currentLight.intensity = dropLightTime;
            if(dropLightTime >= 2f)
            {
                droppedLight = false;
                dropLightTime = 0f;
            }

        }
    }



    void Update()
    {
        followPlayerLight.transform.position = new Vector3(transform.position.x, transform.position.y + .2f, -5f);
        
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
                    //Debug.Log("right to left");
                    playerTransform = Vector3.Lerp(new Vector3(0f, 270f, 0f), new Vector3(0f, 90f, 0f), yRotationLerpValue);
                }
                if (facingDirection == Direction.left)
                {
                    //Debug.Log("left to rigght");
                    playerTransform = Vector3.Lerp(new Vector3(0f, 90f, 0f), new Vector3(0f, 270f, 0f), yRotationLerpValue);
                }
                //Debug.Log(yRotationLerpValue);

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
            animator.SetTrigger("Jump");
        }
        if ((pressedJumpTimer > 0) && IsGrounded() == true)
        {
            
            rb.velocity = Vector2.up * jumpForce * blueLightBuff;
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
        rb.velocity = new Vector2(moveInput * speed * blueLightBuff, rb.velocity.y);
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




/*    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetTrigger("Land");
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
            gameManager.PlayerHit();
        if (other.gameObject.CompareTag("BlueLight"))
            blueLightBuff = 1.5f;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BlueLight"))
            blueLightBuff = 1f;
    }

/*    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }*/



    void dropLight()
    {
        if (dropLightTime == 0f)
        {
            if(lightListIndex == 0)
            {   
                if (light1 != null)
                    light1.intensity = 0f;
                light1 = currentLight = Instantiate(deadLight, new Vector3(transform.position.x, transform.position.y, -5f), Quaternion.identity);
                //Destroy(light1);
                lightListIndex++;
            }
            else if (lightListIndex == 1)
            {
                if (light2 != null)
                    light2.intensity = 0f;
                //light2.SetActive(false);
                light2 = currentLight = Instantiate(deadLight, new Vector3(transform.position.x, transform.position.y, -5f), Quaternion.identity);
                //Destroy(light2);
                lightListIndex++;
            }
            else if (lightListIndex == 2)
            {
                if (light3 != null)
                    light3.intensity = 0f;
                //light3.SetActive(false);
                light3 = currentLight = Instantiate(deadLight, new Vector3(transform.position.x, transform.position.y, -5f), Quaternion.identity);
                //Destroy(light3);
                lightListIndex = 0;
            }



            //lightList[lightListIndex] = currentLight = Instantiate(deadLight, new Vector3(transform.position.x, transform.position.y, -5f), Quaternion.identity);
            
           // if (lightListIndex == 2)
            //    lightListIndex = 0;


            droppedLight = true;
        }
    }

    private bool IsGrounded()
    {

        if ((Physics.OverlapSphere(coll.bounds.center, .5f, jumpableGround)).Length > 0)
        {
            grounded = true;
            animator.SetBool("Falling", false);
        }
        else
        {
            grounded = false;
            animator.SetBool("Falling", true);
        }
        return grounded;

    }


}
