using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandKind
{
	None, Left, Right, Both, Any
}


public class xrMotion : MonoBehaviour
{
	[Header("Move and rotate")]
	[Range(0, 10)]
	public float speed = 1;

	public float rotStep = 10;
	public float rotSleep = 0.3f;

	Rigidbody rigi;
	Vector3 move;
	Transform cameraMain;
	bool sleep;


	void Start()
	{
		rigi = GetComponent<Rigidbody>();
		cameraMain = Camera.main.transform;

	}

	private void Update()
	{
		Move();
		Rotate();
	}

	private void FixedUpdate()
	{

		if (move == Vector3.zero)
		{
			rigi.velocity = Vector3.zero;
			rigi.angularVelocity = Vector3.zero;
			CapsuleCollider coll = GetComponent<CapsuleCollider>();
			coll.center = new Vector3(cameraMain.localPosition.x, coll.center.y, cameraMain.localPosition.z);
		}

		rigi.velocity = move * 50f;

	}

	private void Move()
	{
		//if (!xrHand.LeftHand.isEmpty || !xrHand.RightHand.isEmpty) return;

		move = Vector3.zero;
		move.x = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f ? Input.GetAxis("Horizontal") * 0.8f : 0;
		move.z = Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f ? Input.GetAxis("Vertical") * 0.8f : 0;

		move = Quaternion.AngleAxis(cameraMain.eulerAngles.y, Vector3.up) * move * Time.deltaTime * speed;
	}

	void Rotate()
	{
		if (sleep) return;
		Vector3 rotate = Vector3.zero;
		float angle = 0;
		if (Input.GetKey(KeyCode.E) || Input.GetAxis("Rotate") > 0.3f)
		{
			angle = rotStep;
		}
		if (Input.GetKey(KeyCode.Q) || Input.GetAxis("Rotate") < -0.3f)
		{
			angle = -rotStep;
		}
		if (Mathf.Abs(angle) > 0 )
		{
			transform.Rotate(0, angle, 0);
			if (rotSleep > 0) StartCoroutine(Sleep());
		}
	}

	IEnumerator Sleep()
	{
		sleep = true;
		yield return new WaitForSeconds(rotSleep);
		sleep = false;
	}

	#region INPUT
	const string leftTrigger = "joystick button 14";
	const string rightTrigger = "joystick button 15";
	public static bool GetTrigger(HandKind kind)
	{
		if (kind == HandKind.None)
		{
			return !Input.GetKey(leftTrigger) && !Input.GetKey(rightTrigger);
		}
		if (kind == HandKind.Both)
		{
			return Input.GetKey(leftTrigger) && Input.GetKey(rightTrigger);
		}

		if ((kind == HandKind.Left || kind == HandKind.Any) && Input.GetKey(leftTrigger)) return true;
		if ((kind == HandKind.Right || kind == HandKind.Any) && Input.GetKey(rightTrigger)) return true;

		return false;
	}
	public static bool GetTriggerDown(HandKind kind)
	{
		if (kind == HandKind.None)
		{
			return !Input.GetKeyDown(leftTrigger) && !Input.GetKeyDown(rightTrigger);
		}
		if (kind == HandKind.Both)
		{
			return Input.GetKeyDown(leftTrigger) && Input.GetKeyDown(rightTrigger);
		}

		if ((kind == HandKind.Left || kind == HandKind.Any) && Input.GetKeyDown(leftTrigger)) return true;
		if ((kind == HandKind.Right || kind == HandKind.Any) && Input.GetKeyDown(rightTrigger)) return true;

		return false;
	}
	public static bool GetTriggerUp(HandKind kind)
	{
		if (kind == HandKind.None)
		{
			return !Input.GetKeyUp(leftTrigger) && !Input.GetKeyUp(rightTrigger);
		}
		if (kind == HandKind.Both)
		{
			return Input.GetKeyUp(leftTrigger) && Input.GetKeyUp(rightTrigger);
		}

		if ((kind == HandKind.Left || kind == HandKind.Any) && Input.GetKeyUp(leftTrigger)) return true;
		if ((kind == HandKind.Right || kind == HandKind.Any) && Input.GetKeyUp(rightTrigger)) return true;

		return false;
	}



	#endregion

}
