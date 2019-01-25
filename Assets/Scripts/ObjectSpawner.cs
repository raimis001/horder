using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public Transform spawnPoint;

	void Start ()
    {
		
	}

	void Update ()
    {
		
	}

    void SpawnObject()
    {
        float objScaleX, objScaleY, objScaleZ;
        GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newCube.transform.position = spawnPoint.position;
    }
}
