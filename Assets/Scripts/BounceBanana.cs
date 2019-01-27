using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBanana : MonoBehaviour
{
	public AudioSource sound;

	private bool canSound = true;

    void Update()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude < 0.5f)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 3f);
        }
    }
	private void OnCollisionEnter(Collision collision)
	{
		if (!canSound) return;
		if (sound)
		{
			sound.Play();
		}
	}
	IEnumerator WaitSound()
	{
		canSound = false;
		yield return new WaitForSeconds(3);
		canSound = true;
	}
}
