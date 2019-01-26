using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObjectKind
{
	None, Dish, Trash, Table
}


public class ObjectItem : MonoBehaviour
{
	public ObjectKind kind;

	public bool canRotateXZ;

 public bool IsInplace()
	{
		if (GetComponent<Rigidbody>().isKinematic) return false;

		if (kind == ObjectKind.Table)
		{
			float yy = transform.localPosition.y;
			if (yy < 0.9f || yy > 1.3f) return false;

			Vector3 angle = AbsAngle(transform.localEulerAngles);

			if (angle.x > 5 && angle.x < 355) return false;
			if (angle.z > 5 && angle.z < 355) return false;

			return true;
		}

		if (kind != ObjectKind.None) return false;

		if (canRotateXZ) return true;


		float y = transform.localEulerAngles.y;
		while (y >= 360) y -= 360;
		while (y < 0) y += 360;

		if (y < 5) return true;
		if (y < 95) return Mathf.Abs(y - 90) < 10;
		if (y < 185) return Mathf.Abs(y - 180) < 10;
		if (y < 275) return Mathf.Abs(y - 270) < 10;

		return false;
	}

	private Vector3 AbsAngle(Vector3 angle)
	{
		Vector3 result = angle;

		while (result.x < 0f) result.x += 360f;
		while (result.x >= 360f) result.x -= 360f;

		while (result.y < 0f) result.y += 360f;
		while (result.y >= 360f) result.y -= 360f;

		while (result.z < 0f) result.z += 360f;
		while (result.z >= 360f) result.z -= 360f;

		return result;
	}
	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("Collsion:" + collision.collider.name, collision.collider.gameObject);
	}
}
