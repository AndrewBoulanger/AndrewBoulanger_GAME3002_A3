using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private Vector3 axis = Vector3.up;

    private Rigidbody m_rb = null;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.angularDrag = 0.0f;
       // m_rb.angularVelocity = axis * speed;
        m_rb.AddTorque(axis * speed, ForceMode.Force);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
