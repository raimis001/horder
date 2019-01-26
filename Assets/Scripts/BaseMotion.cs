using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMotion : MonoBehaviour
{
	protected GameObject pickedUpObject;

    [SerializeField] protected float throwStrength;
    [SerializeField] protected Image throwStrImg;

    protected void Rotate(Vector3 delta)
	{
        pickedUpObject.transform.Rotate(Vector3.up, -delta.z, Space.World);
        pickedUpObject.transform.Rotate(transform.right, delta.x, Space.World);      
	}

	protected void Throw()
	{
        pickedUpObject.GetComponent<BaseItem>().itemThrown = true;
        pickedUpObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 2 * (throwStrength * 10), ForceMode.Impulse);

        pickedUpObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        pickedUpObject.GetComponent<Rigidbody>().useGravity = true;

        pickedUpObject = null;

        throwStrength = 0;
        throwStrImg.fillAmount = 0;
    }
}
