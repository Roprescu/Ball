using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce;
    public int maxSpeed;
    public int defaultJump;
    public int maxJump;
    public int gravityPower;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public Vector3 jump;
    public bool isGrounded;
    public int jumpCount;
    public int PickUp;
    



    private int count;
    private Rigidbody rb;
    private float m_HorizontalMovementValue;
    private float m_VerticalMovementValue;
    
    
    
   
    void Start()
    {
        PickUp = GameObject.FindGameObjectsWithTag("PickUp").Length;
        rb = GetComponent<Rigidbody>();
        count = 0;

        jump = new Vector3(0.0f, 2.0f, 0.0f);

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnCollisionEnter()
    {
            isGrounded = true;
            jumpCount = 0;
            jumpForce = defaultJump;

    }

    void OnCollisionExit()
    {
        isGrounded = false;
    }
    
    void OnMove(InputValue _movementValue)
    {
        Vector2 movementVector = _movementValue.Get<Vector2>();

        m_HorizontalMovementValue = movementVector.x;
        m_VerticalMovementValue = movementVector.y;

        
       
    }

    void Jump()
    {
        if(jumpCount < 2 || isGrounded == true)
        {
            if ( Input.GetKeyDown(KeyCode.Space)   )
        {
                Debug.Log("Jump " + isGrounded);
            rb.AddForce(jump * defaultJump, ForceMode.Impulse);
                jumpCount++;
        }
              
        }
    }
   void accelerate()
    {
       
        {
            if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
            {
                if(speed<= maxSpeed)
                {
                    speed++;
                }
                
            }  
            else
            {
                speed = 5;
            }
            
           
        }

       
    }

    void Update()
    {
        Jump();
        
    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Physics.gravity*gravityPower, ForceMode.Acceleration);
        accelerate();
        Vector3 movement = new Vector3(m_HorizontalMovementValue, 0.0f, m_VerticalMovementValue);
        rb.AddForce(movement * speed);
    }

    void SetCountText()
    {
       
        countText.text = "Count: " + count.ToString();
        if(count == PickUp)
        {
            winTextObject.SetActive(true);
            
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
         
        }

        if (other.gameObject.CompareTag("JumpPad"))
        {
            jumpForce = jumpForce + 5;
            jumpCount = 1;
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            if (jumpForce >= maxJump)
            {
                jumpForce = maxJump;
            }       
        }  
    }
 
}
