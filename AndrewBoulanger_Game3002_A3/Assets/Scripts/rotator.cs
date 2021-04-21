using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class that rotates simple objects like the key
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
        m_rb.angularVelocity = axis * speed;

    }
}
