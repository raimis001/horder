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
	

	protected void Throw(Vector3 forward)
	{
		if (pickedUpObject.GetComponent<BaseItem>())
		{
			//Debug.Log("Set throwned");
			pickedUpObject.GetComponent<BaseItem>().itemThrown = true;
			pickedUpObject.GetComponent<BaseItem>().itemPicked = false;
		}

		pickedUpObject.GetComponent<Rigidbody>().AddForce(forward * throwStrength * 20f, ForceMode.Impulse);

		pickedUpObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		pickedUpObject.GetComponent<Rigidbody>().useGravity = true;

        if(throwStrength > 0.1 && throwSoundSource)
        {
            throwSoundSource.PlayDelayed(0.1f);
        }

		pickedUpObject = null;

		throwStrength = 0;
		if (throwStrImg) throwStrImg.fillAmount = 0;
	}

	protected void PickupItem(GameObject pickedUpItem)
	{
		pickedUpObject = pickedUpItem;

		if (pickedUpObject.GetComponent<BaseItem>())
			pickedUpObject.GetComponent<BaseItem>().itemPicked = true;

		pickedUpObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		pickedUpObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		pickedUpObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		pickedUpObject.GetComponent<Rigidbody>().useGravity = false;

    if (pickupSoundSource)    pickupSoundSource.Play();
	}
	private void FixedUpdate()
	{
		if (!pickedUpObject || !pickedUpObject.GetComponent<BaseItem>().itemPicked) return;
		
		pickedUpObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		pickedUpObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}
}
