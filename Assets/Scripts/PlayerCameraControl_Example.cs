using UnityEngine;

public class PlayerCameraControl_Example : MonoBehaviour {

    float xRotation;
    float yRotation;
    float currentXRotation;
    float currentYRoatation;
    float xRotationV;
    float yRoatationV;

    public float Sensitivity = 3f;
    public float smoothDamp = 0.1f;

    GameObject player;

    void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        InitializeScriptReferences();
    }

    /// <summary>
    /// Contains all outside script references
    /// </summary>
    private void InitializeScriptReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update ()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;

        // if (!Cursor.visible)
        // {
        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, smoothDamp);
            currentYRoatation = Mathf.SmoothDamp(currentYRoatation, yRotation, ref yRoatationV, smoothDamp);

            xRotation -= Input.GetAxis("Mouse Y") * Sensitivity;
            yRotation += Input.GetAxis("Mouse X") * Sensitivity;

            xRotation = Mathf.Clamp(xRotation, -60, 60);

            transform.rotation = Quaternion.Euler(currentXRotation, currentYRoatation, 0);
            player.transform.localRotation = Quaternion.Euler(0, currentYRoatation, 0);
        //}
    }
}
