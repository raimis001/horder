using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoPath : MonoBehaviour
{
	public Transform godzila;
	public Transform[] path;

	public AudioSource sound;

	private int currentPath;

	private void Start()
	{
		godzila.transform.position = path[0].position;
		godzila.transform.LookAt(path[1].position);
		currentPath = 1;
		InvokeRepeating("DinoSound", 1, 5);
	}

	private void Update()
	{
		godzila.transform.position = Vector3.MoveTowards(godzila.transform.position, path[currentPath].position, Time.deltaTime / 2f);
		godzila.transform.LookAt(path[currentPath].position);

		if ((godzila.transform.position - path[currentPath].position).magnitude < 0.1f)
		{
			currentPath++;
			if (currentPath >= path.Length) {
				godzila.transform.position = path[0].position;
				godzila.transform.LookAt(path[1].position);
				currentPath = 1;
			}
		}
	}
	private void DinoSound()
	{
		if (sound) sound.Play();
	}
}
