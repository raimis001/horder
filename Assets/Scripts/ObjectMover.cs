using UnityEngine;

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
			if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactDistance, layerMask)) return;

            base.pickedUpObject = hit.transform.gameObject;

            pickedUpObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            pickedUpObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            pickedUpObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pickedUpObject.GetComponent<Rigidbody>().useGravity = false;

            return;
		}

		if (Input.GetMouseButtonUp(0))
		{
            if (base.pickedUpObject)
            {
                Throw();
            }
		}

		if (Input.GetMouseButton(2))
		{
			Rotate(new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")) * objRotateSpeed);
			return;
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
