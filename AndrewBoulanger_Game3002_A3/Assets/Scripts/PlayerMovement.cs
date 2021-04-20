using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//moving, jumping, and falling
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float accelerationRate = 35f;
    [SerializeField] private float decelerationRate = 3f;
    [SerializeField] private float maxSpeed = 5f;
    private float fastSpeed;
    private float slowSpeed;
    private float speedClamp;

    [SerializeField] private float jumpMultiplier = 4f;
    private int numJumps = 2;

    private Vector3 moveInput = Vector3.zero;

    private Rigidbody m_rb = null;

    [SerializeField] private Vector3 resetPosition;

    [SerializeField] private float climbingNormal = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        speedClamp = maxSpeed;
        fastSpeed = maxSpeed * 1.5f;
        slowSpeed = maxSpeed * 0.75f;
    }

    void Update()
    {
        //receives input (also calls jump)
        GetInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyHorizontalMovement();
    }

    void GetInput()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        //jump input
        if (Input.GetKeyDown(KeyCode.Space) && numJumps > 0)
        {
            Jump();
        }
    }

    void Jump()
    {
        if (numJumps > 0)
        {
            numJumps--;
            m_rb.AddForce(Vector3.up * jumpMultiplier, ForceMode.Impulse);
        }
    }

    void ApplyHorizontalMovement()
    {
        //receiving input
        if (moveInput.x != 0)
        {
            //reduce velocity when changing directions
            if (Mathf.Sign(moveInput.x) != Mathf.Sign(m_rb.velocity.x))
            {
                m_rb.velocity = new Vector3(m_rb.velocity.x * 0.2f, m_rb.velocity.y, 0.0f);
            }
            
            //only add force when you aren't at max speed
            if (Mathf.Abs(m_rb.velocity.x) < speedClamp)
            {
                m_rb.AddForce(moveInput * accelerationRate, ForceMode.Force);
                Vector3.ClampMagnitude(m_rb.velocity, speedClamp);
            }
        }
        //no input, but still moving
        else if(m_rb.velocity.x != 0)
        {
            //slow down the character
            m_rb.AddForce(Vector3.right * -m_rb.velocity.x * decelerationRate, ForceMode.Force);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //on flat ground = restore jumps (any surface less than ~40 degrees)
        if (other.contacts[0].normal.y >= climbingNormal)
        {
            numJumps = 2;
        }

        //with hazards (ie. spikes) = return to respawn point
        if (other.gameObject.CompareTag("Hazard"))
        {
            transform.position = resetPosition;
        }


        //with thinWall from below = let the player pass through
        if (other.gameObject.layer == LayerMask.NameToLayer("thinWalls") && other.contacts[0].normal.y < climbingNormal)  
        { 
            other.collider.isTrigger = true;
            m_rb.AddForce(-other.impulse, ForceMode.Impulse);
        }

    }

    void OnCollisionStay(Collision other)
    {
        //if touching a thinWall & pressing down let the player pass through it
        if (other.gameObject.layer == LayerMask.NameToLayer("thinWalls"))
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                other.collider.isTrigger = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //speed up trigger raises max speed
        if (other.gameObject.CompareTag("SpeedUp"))
        {
            speedClamp = fastSpeed;
        }
        //slowdown trigger lowers max speed
        if (other.gameObject.CompareTag("SlowDown"))
        {
            speedClamp = slowSpeed;
        }

        //with checkpoints = set new respawn point
        if(other.gameObject.CompareTag("Checkpoint"))
        {
            resetPosition = other.transform.position;
        }
    }
    void OnTriggerExit(Collider other)
    {
        //leaving speed modifiers = restore max speed to default
        if (other.gameObject.CompareTag("SpeedUp") || other.gameObject.CompareTag("SlowDown") )
        {
            speedClamp = maxSpeed;
        }

        //passing through a thin wall = restore it's collision
        if (other.gameObject.layer == LayerMask.NameToLayer("thinWalls"))
        {
            other.isTrigger = false;
        }
      
    }

}
