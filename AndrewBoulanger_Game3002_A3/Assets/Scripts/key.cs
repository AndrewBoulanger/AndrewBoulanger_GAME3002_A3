using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    [SerializeField] private byte keyMask;

    public byte getKey()
    {
        gameObject.SetActive(false);
        return keyMask;
    }
}
