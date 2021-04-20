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

    [SerializeField] private float jumpMultiplier = 4f;
    private int numJumps = 2;

    private Vector3 moveInput = Vector3.zero;

    private Rigidbody m_rb = null;

    [SerializeField] private Vector3 resetPosition;


    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        handleInput();
    }

    void GetInput()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        //jump input
        if (Input.GetKeyDown(KeyCode.Space) && numJumps > 0)
        {
            numJumps--;
            m_rb.AddForce(Vector3.up * jumpMultiplier, ForceMode.Impulse);
        }
    }
    void handleInput()
    {
        if (moveInput.x != 0)
        {
            //reduce velocity when changing directions
            if (Mathf.Sign(moveInput.x) != Mathf.Sign(m_rb.velocity.x))
            {
                m_rb.velocity = new Vector3(m_rb.velocity.x * 0.2f, m_rb.velocity.y, 0.0f);
            }
            
            if(Mathf.Abs(m_rb.velocity.x) < maxSpeed)
                m_rb.AddForce(moveInput * accelerationRate, ForceMode.Force);
        }
        else if(m_rb.velocity.x != 0) //no input, but not stopped
        {
            m_rb.AddForce(Vector3.right * -m_rb.velocity.x * decelerationRate, ForceMode.Force);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.contacts[0].normal == Vector3.up)
        {
            numJumps = 2;
        }

        if (other.gameObject.CompareTag("Hazard"))
        {
            transform.position = resetPosition;
        }

        if(other.gameObject.CompareTag("Checkpoint"))
        {
            resetPosition = other.transform.position;
        }
    }



}
