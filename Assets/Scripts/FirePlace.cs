using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlace : MonoBehaviour
{
	readonly Dictionary<Collider, float> items = new Dictionary<Collider, float>();



	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer != LayerMask.NameToLayer("MovableObjects")) return;

		if (items.TryGetValue(other, out float time))
		{
			time += Time.deltaTime;
			items[other] = time;
			if (time > 30)
			{
				items.Remove(other);
				if (other.GetComponent<ObjectSmashingManager>())
				{
					other.GetComponent<ObjectSmashingManager>().SmashItemMesh(false);
				}
				Destroy(other.gameObject);
			}
			return;
		}

		items.Add(other, 0);
	}
}
