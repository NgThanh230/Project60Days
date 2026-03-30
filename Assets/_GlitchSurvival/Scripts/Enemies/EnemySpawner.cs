using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota; //tổng số quái spawn trong 1 wave
        public float spawnInterval; //thời gian để spawn quái
        public float spawnCount; //đếm số quái đã spawn
    }
    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;// số thứ tự của quái khi spawn
        public int spawnCount;// số quái khi spawn
        public GameObject enemyPrefabs;
    }

    public List<Wave> waves;//list wave
    public int currentWaveCount;//số wave hiện tại

    [Header("Spawner Atributes")]
    float spawnTimer;
    public int enemeiesAlive;
    public int maxEnemiesAllowed;//tối đa quái có thể spawn
    public bool maxEnemiesReached = false; //boolean dành để báo hiệu khi nào đạt tối đa quái đã spawn
    public float waveInterval; //thời gian giữa các wave

    [Header("Spawn Positions")]
    public List<Transform> SpawnPoints; //list chứa các điểm spawn quái

    Transform player;
    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>().transform;
        CalculateWaveQuota();
        
    }
    void Update()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        //coroutine cho dừng theo waveinterval
        yield return new WaitForSeconds(waveInterval);
        //index đếm số từ 0 nên phải trừ 1
        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }
    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;//
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        //check wavequota đã đủ số spawn chưa
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            //duyệt từng mảng enemygroup trong waves với tham số currentWaveCount
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //giới hạn số lượng quái có thể spawn trong 1 lần
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if (enemeiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }
                    //spawn random theo điểm đã có sẵn
                    Instantiate(enemyGroup.enemyPrefabs, player.position + SpawnPoints[Random.Range(0, SpawnPoints.Count)].position, Quaternion.identity);
                    ////tạo điểm spawn cách người chơi ngẫu nhiên cả x và y từ -10 tới 10
                    //Vector2 spawnPosition = new Vector2(player.transform.position.x + Random.Range(-10f, 10f), player.transform.position.y + Random.Range(-10f, 10f));
                    //Instantiate(enemyGroup.enemyPrefabs, spawnPosition, Quaternion.identity);
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemeiesAlive++;
                }
            }
        }

        //reset maxEnemiesReached nếu quái ít hơn số lượng tối đa có thể spawn
        if (enemeiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }
    public void OnEnemyKilled()
    {
        enemeiesAlive--;
    }
}
