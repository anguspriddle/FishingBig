using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public Transform spawnPoint; // Assign the empty GameObject's transform here
    public int maxFishCount = 3;
    public float respawnTime = 5f;

    private int currentFishCount = 0;

    private void Start()
    {
        // Start spawning fish
        SpawnFish();
    }

    private void SpawnFish()
    {
        if (currentFishCount < maxFishCount)
        {
            Vector3 spawnOffset = GetRandomSpawnPosition();
            Vector3 spawnPosition = spawnPoint.position + spawnOffset;
            GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
            newFish.GetComponent<Animator>().Play("fishANIMONE");
            currentFishCount++;

            // Schedule the next spawn
            Invoke("SpawnFish", respawnTime);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float minX = -1931f;
        float maxX = 6598f;
        float minY = -587f;
        float maxY = 100f; // Adjust this to your desired height range
        float minZ = -1033f;
        float maxZ = 6900f;

        Vector3 spawnOffset = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            Random.Range(minZ, maxZ)
        );

        return spawnOffset;
    }
}
