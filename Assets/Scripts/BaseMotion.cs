using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMotion : MonoBehaviour
{
	protected GameObject pickedUpObject;

    [SerializeField] protected AudioSource pickupSoundSource;
    [SerializeField] protected AudioSource throwSoundSource;

    [SerializeField] protected float throwStrength;
    [SerializeField] protected Image throwStrImg;

	protected void Rotate(Vector3 delta)
	{
		pickedUpObject.transform.Rotate(Vector3.up, -delta.z, Space.World);
		pickedUpObject.transform.Rotate(transform.right, delta.x, Space.World);
	}

	protected void Throw()
	{
		pickedUpObject.GetComponent<BaseItem>().itemThrown = true;
		pickedUpObject.GetComponent<BaseItem>().itemPicked = false;

		pickedUpObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 2 * (throwStrength * 10), ForceMode.Impulse);

		pickedUpObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		pickedUpObject.GetComponent<Rigidbody>().useGravity = true;

        if(throwStrength > 0.1)
        {
            //Debug.Log("play throw sound");
            throwSoundSource.PlayDelayed(0.1f);
        }

		pickedUpObject = null;

		throwStrength = 0;
		throwStrImg.fillAmount = 0;
	}

	protected void PickupItem(GameObject pickedUpItem)
	{
		pickedUpObject = pickedUpItem;

		pickedUpObject.GetComponent<BaseItem>().itemPicked = true;
		pickedUpObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		pickedUpObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		pickedUpObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		pickedUpObject.GetComponent<Rigidbody>().useGravity = false;

        //Debug.Log("play pickup sound");
        pickupSoundSource.Play();

	}
}
