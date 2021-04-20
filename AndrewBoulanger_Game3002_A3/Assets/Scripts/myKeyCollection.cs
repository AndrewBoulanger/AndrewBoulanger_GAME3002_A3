using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum keyTypes
{
    None = 0x0,
    One = 0x1,
    Two = 0x2
}

public class myKeyCollection : MonoBehaviour
{

    private byte keys = 0x0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("key"))
        {
           keys |= other.gameObject.GetComponent<key>().getKey();

           print(keys);
        }
    }

    public byte GetKeys()
    {
        return keys;
    }
}
