using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMotion : MonoBehaviour
{
	protected GameObject takeObject;

	protected void Rotate(Vector3 delta)
	{
		takeObject.transform.Rotate(Vector3.up, -delta.y, Space.World);
		takeObject.transform.Rotate(transform.right, delta.x, Space.World);
	}

	protected void Throw()
	{
		Rigidbody body = takeObject.GetComponent<Rigidbody>();

		body.useGravity = true;
		body.AddForce(Camera.main.transform.forward * 20f, ForceMode.Impulse);

		takeObject = null;
	}
}
