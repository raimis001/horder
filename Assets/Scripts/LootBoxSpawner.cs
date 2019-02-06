using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxSpawner : MonoBehaviour
{
    [Header("Main Settings")]
    public GameObject lootBoxObj; //Lootbox prefab
    public float initialWait = 10f; //How long it takes until lootboxes start spawning
    public float repeatingWait = 10f; //How frequently lootboxes will spawn

    [Header("Additional Settings")]
    public bool isSpawnerRandomized = true; //Should lootbox spawning be randomized?
    public float randomizationRange = 5f; //Randomization time range added to spawn times
    public bool checkSpawnCollision = true; //If true, stops spawning new objects if there's an object already within the spawner collider

    bool spawnInitialization = true;
    bool spawnBlocked = false;
    float spawnUnblockTimer = 1;
    readonly WaitForSeconds shortWait = new WaitForSeconds(1);

    void Start()
    {
        StartCoroutine(LootBoxSpawn());
    }

    private void OnTriggerStay(Collider other)
    {
        if (!checkSpawnCollision) { return; }
        spawnUnblockTimer = 1;
        spawnBlocked = true;
    }

    private void Update()
    {
        if (!spawnBlocked) { return; }
        spawnUnblockTimer -= 0.6f * Time.deltaTime;
        if (spawnUnblockTimer > 0) { return; }
        StartCoroutine(LootBoxSpawn());
        spawnBlocked = false;
    }

    IEnumerator LootBoxSpawn()
    {
        if (spawnInitialization)
        {
            spawnInitialization = false;
            yield return new WaitForSeconds(initialWait);
            GameObject newStartLootBox = Instantiate(lootBoxObj, transform.position, transform.rotation);
            StartCoroutine(LootBoxSpawn());
        }
        if (isSpawnerRandomized)
        {
            float randomTime = Random.Range(-randomizationRange / 2, randomizationRange / 2);
            yield return new WaitForSeconds(repeatingWait + randomTime);
        }
        else
        {
            yield return new WaitForSeconds(repeatingWait);
        }
        if (!spawnBlocked)
        {
            GameObject newLootBox = Instantiate(lootBoxObj, transform.position, transform.rotation);
            StartCoroutine(LootBoxSpawn());
        }
    }
}
