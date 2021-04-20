using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DoorAccess : MonoBehaviour
{
    [SerializeField] private byte accessKey;
    private float swing = 90f;

    [SerializeField]
    private Rigidbody door = null;

    [SerializeField] private Image LockedUI;
// Start is called before the first frame update
    void Start()
    {
       
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          byte currentKeys = other.GetComponent<myKeyCollection>().GetKeys() ;

          //if the key collection contains the access key, open
          if ((currentKeys & accessKey) == accessKey)
          {
              door.isKinematic = false;
          }
          else
          {
              LockedUI.gameObject.SetActive(true);
          }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (LockedUI.gameObject.activeSelf)
        {
            LockedUI.gameObject.SetActive(false);
        }
    }
}
