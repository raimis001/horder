using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorAxis
{
    X, Y, Z
}

public class DoorOpenScript : MonoBehaviour
{
    Quaternion targetRotation;
    Camera playerCam;
    bool doorOpen = false;
    bool doorRotating = false;
    float doorRotationValue;
    float diff;

    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smooth = 2f;
    public float distanceToCheck = 3f;
    public DoorAxis doorRotateAxis;

    void Start()
    {
        playerCam = Camera.main;
    }

    void Update()
    {
        Vector3 offset = playerCam.transform.position - transform.position;
        float sqrLen = offset.sqrMagnitude;
        if (sqrLen <= distanceToCheck && !doorOpen || sqrLen >= distanceToCheck && doorOpen)
            DoorInteract();

        if (!doorRotating) return;

        switch (doorRotateAxis)
        {
            case DoorAxis.X:
                targetRotation = Quaternion.Euler(doorRotationValue, transform.localEulerAngles.y, transform.localEulerAngles.z);
                diff = transform.localRotation.eulerAngles.x - targetRotation.eulerAngles.x;
                break;
            case DoorAxis.Y:
                targetRotation = Quaternion.Euler(transform.localEulerAngles.y, doorRotationValue, transform.localEulerAngles.z);
                diff = transform.localRotation.eulerAngles.y - targetRotation.eulerAngles.y;
                break;
            case DoorAxis.Z:
                targetRotation = Quaternion.Euler(transform.localEulerAngles.y, transform.localEulerAngles.y, doorRotationValue);
                diff = transform.localRotation.eulerAngles.z - targetRotation.eulerAngles.z;
                break;
            default:
                break;
        }   
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);

        if (Mathf.Abs(diff) <= 0.2f)
        {
            transform.localRotation = targetRotation;
            doorRotating = false;
        }
    }

    void DoorInteract()
    {
        if (doorOpen)
        {
            doorRotationValue = doorCloseAngle;
            doorOpen = false;
        }
        else
        {
            doorRotationValue = doorOpenAngle;
            doorOpen = true;
        }
        doorRotating = true;
    }
}
