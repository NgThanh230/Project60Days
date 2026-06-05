using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
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

    [Header("Damage FeedBack")]
    public Color damageColor = new Color(1, 0, 0, 1);
    public float damageFlashDuration = 0.2f;
    public float deathFadeTime = 0.6f;
    Color originalColor;
    SpriteRenderer sr;
    EnemyMovement movement;
    void Awake()
    {
        currentDamage = enemyData.Damage;
        currentHealth = enemyData.MaxHealth;
        currentMoveSpeed = enemyData.MoveSpeed;
    }
    private void Start()
    {
        player = FindAnyObjectByType<PlayerStats>().transform;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        movement = GetComponent<EnemyMovement>();
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) >= deSpawnDistance)
        {
            ReturnEnemy();
        }
       
    }
    public void TakeDamage(float damage, Vector2 sourcePosition, float knockbackForce = 5f, float knockbackDuration = 0.2f)
    {
        currentHealth -= damage;
        StartCoroutine(DamageFlash());
        
        //tạo popup khi enemy dính dame
        if (damage > 0)
        {
            GameManager.GenerateFloatingText(Mathf.FloorToInt(damage).ToString(), transform);
        }
        //thêm knockback nếu knockbackForce không phải là 0
        if (knockbackForce > 0)
        {
            //lấy hướng knockback
            Vector2 dir = (Vector2)transform.position - sourcePosition;
            movement.Knockback(dir.normalized * knockbackForce, knockbackDuration);
        }
        if (currentHealth <= 0)
        {
            Kill();
        }
    }
    IEnumerator DamageFlash()
    {
        sr.color = damageColor;
        yield return new WaitForSeconds(damageFlashDuration);
        sr.color = originalColor;
    }

    public void Kill()
    {
        StartCoroutine(KillFade());
    }
    IEnumerator KillFade()
    {
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0, origAlpha = sr.color.a;

        while (t < deathFadeTime)
        {
            yield return w;
            t += Time.deltaTime;

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (1 - t / deathFadeTime) * origAlpha);
        }
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
    //Coroutine cho phần hiệu ứng quái dính dame 
    
   
    
}
