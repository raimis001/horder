using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extigusher : BaseItem
{
	public GameObject steam;

	private float engiTime;
	private void Update()
	{
		if (steam.activeInHierarchy && !itemPicked) steam.SetActive(false);
		if (!steam.activeInHierarchy && itemPicked) steam.SetActive(true);

		if (!steam.activeInHierarchy) return;

		steam.transform.rotation = Camera.main.transform.rotation;

		Ray ray = new Ray(steam.transform.position, steam.transform.forward);
		Debug.DrawRay(ray.origin, ray.direction * 3);
		if (Physics.Raycast(ray, out RaycastHit hit, 5, LayerMask.GetMask("Effects")))
		{
			if (engiTime <= 0) engiTime = 10;
			engiTime -= Time.deltaTime;
			if (engiTime <= 0)
			{
				Destroy(hit.collider.gameObject);
			}
		}
	}
}
