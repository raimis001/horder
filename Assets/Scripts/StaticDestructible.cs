using UnityEngine;

public class StaticDestructible : MonoBehaviour
{
    [SerializeField] GameObject destroyedVersion;
    [SerializeField] Vector3 rotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("MovableObjects"))
        {
            if (other.GetComponent<ObjectSmashingManager>().itemThrown)
            {           
                Debug.Log("Window is smashed");
                SetUpDestroyedVer();
            }
        }
    }

    private void SetUpDestroyedVer()
    {
        GameObject currDestVer = Instantiate(destroyedVersion);
        currDestVer.transform.position = new Vector3(transform.position.x, transform.position.y - 0.11f, transform.position.z);
        currDestVer.transform.eulerAngles = rotation;
        Destroy(gameObject);
    }
}
