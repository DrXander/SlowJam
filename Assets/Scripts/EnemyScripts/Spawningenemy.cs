using UnityEngine;

public class Spawningenemy : MonoBehaviour
{
    [SerializeField] 
    private GameObject enemyPrefab;

    [SerializeField]
    private float minimumSpawnTime;

    [SerializeField]
    private float maximumSpawnTime;

    private float TimeUntilNextSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SetTimeUnitlSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        TimeUntilNextSpawn -= Time.deltaTime;
        
        if (TimeUntilNextSpawn <= 0f)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            SetTimeUnitlSpawn();
        }
    }

    private void SetTimeUnitlSpawn()
    {
        TimeUntilNextSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}
