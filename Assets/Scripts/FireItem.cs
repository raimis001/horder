using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireItem : BaseItem
{
	public GameObject firePrefab;

	private void OnCollisionEnter(Collision collision)
	{
		if (!itemThrown) return;

		Instantiate(firePrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
