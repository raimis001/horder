using UnityEngine;
using UnityEngine.UI;

public class ObjectMover : BaseMotion
{
	[SerializeField] float interactDistance = 5f;
	[SerializeField] float objRotateSpeed = 20f;

	[SerializeField] LayerMask layerMask;
	[SerializeField] Transform playerHand;
	[SerializeField] Transform player;

	private void Start()
	{
		throwStrImg.fillAmount = 0;
	}
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactDistance, layerMask))
			{
				PickupItem(hit.transform.gameObject);
				pickedUpObject.transform.SetParent(player);
				pickedUpObject.transform.localPosition = playerHand.localPosition;
				return;
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (base.pickedUpObject)
			{
				pickedUpObject.transform.SetParent(null);
				Throw(transform.forward);
				return;
			}
		}

		if (Input.GetMouseButton(2))
		{
			Rotate(new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")) * objRotateSpeed);
			return;
		}

		if (!base.pickedUpObject) return;
		//player.position = transform.position;

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
			throwStrImg.fillAmount = throwStrength;
		
	}
	private void LateUpdate()
	{
		if (!base.pickedUpObject) return;

		player.position = transform.position;
		player.forward = transform.forward;
	}
	//private void FixedUpdate()
	//{
	//	if (base.pickedUpObject)
	//	{
	//		base.pickedUpObject.transform.position = playerHand.position;
	//	}
	//}
}
