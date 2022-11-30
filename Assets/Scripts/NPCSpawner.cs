using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject npcPrefab;

    private float cooldown = 2f;
    private float timer = 2;
    private Transform randomSpawnPoint;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            timer = 0;
            SpawnNPC();
        }
    }

    private void SpawnNPC()
    {
        randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
        if (!randomSpawnPoint.GetComponent<NPCDuplicateChecker>().isPlaceTaken())
        {
            Instantiate(npcPrefab, randomSpawnPoint.transform.position, randomSpawnPoint.transform.rotation);
        }
    }
}
