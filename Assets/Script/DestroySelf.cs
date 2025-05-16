using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float waitAmount = 0f;
    void Start()
    {
        Destroy(gameObject, waitAmount);
    }

}
