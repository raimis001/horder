using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour {

    Transform holdingPoint;
    Transform heldObject;
    RaycastHit hit;
    CameraController camControl;
    Rigidbody objRb;
    bool movingObj = false;

    public float interactDistance = 5f;
    public float objRotateSpeed = 20f;
    public LayerMask layerMask;
    public KeyCode moveKey = KeyCode.F;
    public KeyCode rotateKey = KeyCode.R;

    void Start ()
    {
        camControl = GetComponent<CameraController>();
        holdingPoint = new GameObject().transform;
        holdingPoint.SetParent(transform, false);
	}

	void Update ()
    {
        if (Input.GetKeyDown(moveKey))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, layerMask))
            {
                holdingPoint.position = hit.transform.position;
                heldObject = hit.transform;
                objRb = hit.transform.GetComponent<Rigidbody>();
            }
            if (!movingObj)
            {
                objRb.isKinematic = true;
                movingObj = true;
            }
            else
            {
                objRb.isKinematic = false;
                movingObj = false;
            }
        }

        if (movingObj)
        {
            heldObject.position = holdingPoint.position;

            if (Input.GetKey(rotateKey))
            {

                camControl.restrictCamera = true;
                float rotX = objRotateSpeed * Input.GetAxis("Mouse Y");
                float rotY = objRotateSpeed * Input.GetAxis("Mouse X");
                heldObject.Rotate(Vector3.up, -rotY, Space.World);
                heldObject.Rotate(transform.right, rotX, Space.World);
            }
            else
            {
                camControl.restrictCamera = false;
                float rotY = camControl.sensitivityX * Input.GetAxisRaw("Mouse X");
                heldObject.Rotate(Vector3.up, rotY, Space.World);
            }
        }
    }
}
