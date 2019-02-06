using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxScript : MonoBehaviour
{

    public List<GameObject> lootBoxItems;

    public float distanceToCheck = 3f;

    Camera playerCam;
    bool playerIsNear = false;

    void Start()
    {
        playerCam = Camera.main;
    }

    void Update()
    {
        Vector3 offset = playerCam.transform.position - transform.position;
        float sqrLen = offset.sqrMagnitude;
        if (sqrLen <= distanceToCheck)
        { playerIsNear = true; }
        else
        { playerIsNear = false; }

        if (Input.GetKeyDown(KeyCode.F) && playerIsNear)
        { SpawnObjects(); }
    }

    void SpawnObjects()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject newObject = Instantiate(lootBoxItems[Random.Range(0, lootBoxItems.Count)], transform.position, Quaternion.Euler(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359)));
        }
        Destroy(gameObject);
    }

    void OnGUI()
    {
        if (!playerIsNear) { return; }
        GUI.Label(new Rect((Screen.width / 2) - 50, (Screen.height / 2) + 50, 100, 20), "Press F to unbox");
    }
}
