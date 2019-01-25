using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField] GameObject primitiveCube;

    void Start()
    {
        InvokeRepeating("SpawnPrimitive", 0, 3);
    }

    private void SpawnPrimitive(GameObject primitive)
    {
        GameObject currPrimitive = Instantiate(primitive);
        Vector3 primitiveLocalScale = currPrimitive.transform.localScale; 
        currPrimitive.transform.localScale = ResizePrimitive();
        currPrimitive.name = "Primitive";
        currPrimitive.transform.position = new Vector3(0, 10, 0);

    }

    private Vector3 ResizePrimitive()
    {
        return new Vector3(Random.Range(1,5), Random.Range(1,5), Random.Range(1,5));
    }
}
