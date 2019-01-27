using UnityEngine;
using UnityEngine.UI;

public class ObjectMover : BaseMotion
{
	[SerializeField] float interactDistance = 5f;
	[SerializeField] float objRotateSpeed = 20f;

	[SerializeField] LayerMask layerMask;
	[SerializeField] Transform playerHand;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactDistance, layerMask))
			{
				PickupItem(hit.transform.gameObject);
				return;
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (base.pickedUpObject)
			{
				Throw();
				return;
			}
		}

		if (Input.GetMouseButton(2))
		{
			Rotate(new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")) * objRotateSpeed);
			return;
		}

		if (base.pickedUpObject)
		{
			if (Input.GetMouseButton(1))
			{
				if (throwStrength < 1)
				{
					throwStrength += Time.deltaTime / 1.2f;
				}
			}
			else
			{
				if (throwStrength > 0)
				{
					throwStrength -= Time.deltaTime / 1f;
				}
			}
			throwStrength = Mathf.Clamp(throwStrength, 0, 1);

			//if (Input.GetKeyDown(KeyCode.E))
			//{
			//	throwStrength += 0.1f;
			//	if (throwStrength > 1)
			//	{
			//		throwStrength = 1;
			//	}
			//}
			//if (Input.GetKeyDown(KeyCode.Q))
			//{
			//	throwStrength -= 0.1f;
			//	if (throwStrength < 0)
			//	{
			//		throwStrength = 0;
			//	}
			//}

			throwStrImg.fillAmount = throwStrength;
		}
	}

	private void FixedUpdate()
	{
		if (base.pickedUpObject)
		{
			base.pickedUpObject.transform.position = playerHand.position;
		}
	}
}
