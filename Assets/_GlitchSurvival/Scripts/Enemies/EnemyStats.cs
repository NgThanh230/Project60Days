using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    public float deSpawnDistance = 20f;
    Transform player;

    void Awake()
    {
        currentDamage = enemyData.Damage;
        currentHealth = enemyData.MaxHealth;
        currentMoveSpeed = enemyData.MoveSpeed;
    }
    private void Start()
    {
        player = FindAnyObjectByType<PlayerStats>().transform;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) >= deSpawnDistance)
        {
            ReturnEnemy();
        }
       
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
       
    }
    private void OnDestroy()
    {
        EnemySpawner enemyspawner = FindAnyObjectByType<EnemySpawner>();
        enemyspawner.OnEnemyKilled();
    }
    void ReturnEnemy()
    {
        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        // lấy vị trí của player + với vị trí ngẫu nhiên trong list spawnpoint = vị trí spawn quái mới
        transform.position = player.position + enemySpawner.SpawnPoints[Random.Range(0, enemySpawner.SpawnPoints.Count)].position;
    }
}
