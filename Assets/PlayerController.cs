﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	Rigidbody rb;
	public float speed;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	void FixedUpdate()
	{
		float mH = Input.GetAxis("Horizontal");
		float mV = Input.GetAxis("Vertical");

		Vector3 move = Camera.main.transform.rotation * new Vector3(mH, 0, mV) * speed;
		move.y = rb.velocity.y;
		rb.velocity = move;
	}
	private void Update()
	{
		transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
	}
}


