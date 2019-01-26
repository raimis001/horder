using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMotion : MonoBehaviour
{
	protected GameObject pickedUpObject;

	protected void Rotate(Vector3 delta)
	{
        pickedUpObject.transform.Rotate(Vector3.up, -delta.z, Space.World);
        pickedUpObject.transform.Rotate(transform.right, delta.x, Space.World);
        
	}

	protected void Throw()
	{
        pickedUpObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 20f, ForceMode.Impulse);
        pickedUpObject = null;

        pickedUpObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        pickedUpObject.GetComponent<Rigidbody>().useGravity = true;
    }
}
