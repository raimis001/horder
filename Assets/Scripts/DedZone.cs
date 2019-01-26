using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DedZone : MonoBehaviour
{

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer != LayerMask.NameToLayer("MovableObjects")) return;

		Destroy(other.gameObject, 1);
	}

}
