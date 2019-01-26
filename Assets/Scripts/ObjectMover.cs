﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectMover : MonoBehaviour
{

	Transform holdingPoint;
	Transform heldObject;
	RaycastHit hit;
	CameraController camControl;
	Rigidbody objRb;
	bool movingObj = false;

	public float interactDistance = 5f;
	public float objRotateSpeed = 20f;
	public LayerMask layerMask;
	public KeyCode moveKey = KeyCode.F;
	public KeyCode rotateKey = KeyCode.R;

	private bool canRotateXZ;

	void Start()
	{
		camControl = GetComponent<CameraController>();
		holdingPoint = new GameObject().transform;
		holdingPoint.SetParent(transform, false);
	}

	void Update()
	{
		if (Input.GetKeyDown(moveKey))
		{
			if (heldObject)
			{
				objRb.isKinematic = false;
				movingObj = false;
				heldObject = null;
				objRb = null;
				return;
			}

			if (!Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, layerMask))
			{
				return;
			}

			holdingPoint.position = hit.transform.position;
			heldObject = hit.transform;
			objRb = heldObject.GetComponent<Rigidbody>();
			objRb.isKinematic = true;
			movingObj = true;

			ObjectItem item = heldObject.GetComponent<ObjectItem>();
			if (!item)
			{
				canRotateXZ = true;
				return;
			}

			canRotateXZ = item.canRotateXZ;
			return;
		}

		if (!movingObj) return;

		heldObject.position = holdingPoint.position;

		float rotY;
		if (Input.GetKey(rotateKey))
		{

			camControl.restrictCamera = true;
			
			float rotX = objRotateSpeed * Input.GetAxis("Mouse Y");
			rotY = objRotateSpeed * Input.GetAxis("Mouse X");
			heldObject.Rotate(Vector3.up, -rotY, Space.World);
			if (canRotateXZ)
				heldObject.Rotate(transform.right, rotX, Space.World);
			return;
		}

		camControl.restrictCamera = false;
		rotY = camControl.sensitivityX * Input.GetAxisRaw("Mouse X");
		heldObject.Rotate(Vector3.up, rotY, Space.World);

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
		if (objRb) objRb.isKinematic = false;
		movingObj = false;
		heldObject = null;
		objRb = null;

	}
}
