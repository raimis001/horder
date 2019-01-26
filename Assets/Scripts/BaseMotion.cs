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
	}

    protected void SetupPickupContraints(bool lockContraints)
    {
        if (lockContraints)
        {
            pickedUpObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            pickedUpObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            pickedUpObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            pickedUpObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
