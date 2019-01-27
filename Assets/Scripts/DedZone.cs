using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DedZone : MonoBehaviour
{

	public AudioSource sound;
	public GameObject dedEffect;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer != LayerMask.NameToLayer("MovableObjects")) return;

		if (dedEffect) Instantiate(dedEffect, other.transform.position, Quaternion.identity);

		if (sound) sound.Play();
		Destroy(other.gameObject, 0.5f);
	}

}
