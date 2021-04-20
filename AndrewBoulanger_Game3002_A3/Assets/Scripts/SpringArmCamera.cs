using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArmCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField] public float smoothSpeed = 0.125f;

    [SerializeField]
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 EndPosition = target.position + offset;
        Vector3 LerpedPosition = Vector3.Lerp(transform.position, EndPosition, smoothSpeed);

        transform.position = LerpedPosition;
    }
}
