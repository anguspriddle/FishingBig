using UnityEngine;

public class FishPrefab : MonoBehaviour
{
    public Animator fishAnimator; // Reference to the Animator component
    public float respawnTime = 5f;
    public int maxFishCount = 3;

    private int currentFishCount = 0;

    private void Start()
    {
        fishAnimator = GetComponent<Animator>(); // Assign the Animator component in the Inspector
        SpawnFish();
    }

    public void AnimationRespawnEvent()
    {
        Destroy(gameObject);
        currentFishCount--;

        if (currentFishCount < maxFishCount)
        {
            Invoke("SpawnFish", respawnTime);
        }
    }

    private void SpawnFish()
    {
        Vector3 randomSpawnPoint = GetRandomSpawnPoint();
        GameObject newFish = Instantiate(gameObject, randomSpawnPoint, Quaternion.identity);
        newFish.GetComponent<FishPrefab>().fishAnimator.Play("fishANIMONE");
        currentFishCount++;

        // Respawn timer should be started after spawning a new fish
        Invoke("SpawnFish", respawnTime);
    }



    private Vector3 GetRandomSpawnPoint()
    {
        float minX = -1931f;
        float maxX = 6598f;
        float minY = -587f;
        float maxY = 100f; // Adjust this to your desired height range
        float minZ = -1033f;
        float maxZ = 6900f;

        Vector3 spawnPoint = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            Random.Range(minZ, maxZ)
        );

        return spawnPoint;
    }
}