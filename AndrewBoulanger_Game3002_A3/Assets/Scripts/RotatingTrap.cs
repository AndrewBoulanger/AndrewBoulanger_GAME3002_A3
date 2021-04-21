using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotatingTrap : MonoBehaviour
{
    [SerializeField]
    private Vector3 Force = Vector3.zero;
    
    private Vector3 CenterOfMass = Vector3.zero;

    [SerializeField]
    private Vector3 ForcePoint = Vector3.zero;
    [SerializeField]
    private float maxAngularVelocity = 10f;

    private Vector3 Torque = Vector3.zero;

    private Rigidbody m_rb = null;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.centerOfMass = CenterOfMass;
        m_rb.maxAngularVelocity = maxAngularVelocity;
    }

    private void FixedUpdate()
    {
        Torque = Vector3.Cross(Force, ForcePoint - CenterOfMass);
        m_rb.AddTorque(Torque);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.TransformPoint(CenterOfMass), 0.5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.TransformPoint(ForcePoint), 0.5f);
    }
}
