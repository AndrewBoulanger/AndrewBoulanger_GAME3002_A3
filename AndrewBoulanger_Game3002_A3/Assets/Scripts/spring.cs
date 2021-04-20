using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spring : MonoBehaviour
{

    [SerializeField]
    private float SpringConstant;
    [SerializeField]
    private float DampingConstant;
    [SerializeField]
    private Vector3 Offset;

    private Vector3 m_vRestPos;
    private float m_fMass;
    [SerializeField]
    private Rigidbody m_attachedBody = null;

    private Vector3 m_vForce;
    private Vector3 m_vPrevVel;

    void Awake()
    {
        m_vRestPos = transform.position + Offset;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_fMass = m_attachedBody.mass;

        SpringConstant = CalculateSpringConstant();
    }
    private float CalculateSpringConstant()
    {
        float fDX = (m_vRestPos - m_attachedBody.transform.position).magnitude;

        if (fDX <= 0f)
        {
            return Mathf.Epsilon;
        }

        return (m_fMass * Physics.gravity.y) / (fDX);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateSpringForce();
    }

    private void UpdateSpringForce()
    {
        m_vForce = -SpringConstant * (m_vRestPos - m_attachedBody.transform.position) -
                   DampingConstant * (m_attachedBody.velocity - m_vPrevVel);

        m_attachedBody.AddForce(m_vForce, ForceMode.Acceleration);

        m_vPrevVel = m_attachedBody.velocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_vRestPos, 1f);

        if (m_attachedBody)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_attachedBody.transform.position, 1f);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, m_attachedBody.transform.position);

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, Vector3.one);
        }
    }
}
