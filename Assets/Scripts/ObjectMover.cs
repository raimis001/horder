using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectMover : BaseMotion
{
	[SerializeField] float interactDistance = 5f;
	[SerializeField] float objRotateSpeed = 20f;
	[SerializeField] LayerMask layerMask;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (takeObject)
			{
				return;
			}

			if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactDistance, layerMask))
			{
				return;
			}

			takeObject = hit.transform.gameObject;
			takeObject.GetComponent<Rigidbody>().useGravity = false;
			takeObject.transform.SetParent(Camera.main.transform);

			return;
		}

		if (!takeObject) return;

		if (Input.GetMouseButtonUp(0))
		{
			takeObject.transform.SetParent(null);
			Throw();
		}

		if (Input.GetMouseButton(2))
		{
			Rotate(new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")) * objRotateSpeed);
			return;
		}

	}

	private void OnEnable()
	{
		ObjectAccepter.OnDeleteItem += Destroy;
	}
	private void OnDisable()
	{
		ObjectAccepter.OnDeleteItem -= Destroy;
	}

	public void Destroy(ObjectItem item)
	{
	}
}
