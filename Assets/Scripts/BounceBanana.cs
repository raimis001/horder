using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBanana : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude < 0.5f)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 3f);
        }
    }
}
