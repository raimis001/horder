using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DedZone : MonoBehaviour
{

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer != LayerMask.GetMask("MovableObjects")) return;

		Destroy(other.gameObject, 1);
	}

}
