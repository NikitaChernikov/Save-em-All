using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private Vector2 rangeBordersX;
    [SerializeField] private Vector2 rangeBordersZ;

    private Vector3 range;
    private float timer;
    private float cooldown = 3;

    private void Start()
    {
        StartCoroutine(ChangeCooldown());
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            timer = 0;
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        range.x = Random.Range(rangeBordersX.x, rangeBordersX.y);
        range.y = transform.position.y;
        range.z = Random.Range(rangeBordersZ.x, rangeBordersZ.y);
        Instantiate(asteroidPrefab, range, asteroidPrefab.transform.rotation);
    }

    private IEnumerator ChangeCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
            if (cooldown >= 0.5f) cooldown -= 0.5f;
        }
    }
}
