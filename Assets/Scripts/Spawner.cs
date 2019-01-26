using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[Range(0,5)]
	public float interval = 2;

	public GameObject[] spawnPrefabs;

	// Start is called before the first frame update
	void Start()
	{
		InvokeRepeating("SpawnPrimitive", 0, interval);
	}

	// Update is called once per frame
	void SpawnPrimitive()
	{
		Vector3 pos = new Vector3(Random.Range(1f, 4f), transform.position.y, Random.Range(1f, 6f));
		int prfab = Random.Range(0, spawnPrefabs.Length);

		Instantiate(spawnPrefabs[prfab], pos, Quaternion.identity,transform);
	}
}
