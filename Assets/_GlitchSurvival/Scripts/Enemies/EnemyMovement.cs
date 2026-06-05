using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;
    SpriteRenderer spriteRenderer;
    Vector2 knockbackVelocity;
    float knockbackDuration;
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindAnyObjectByType<PlayerMovement>().transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockbackDuration > 0)
        {
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else
        {
            
            Vector2 targetPos = player.transform.position;
            //enemy chạy từ điểm spawn tới người chơi, *time để đồng bộ thời gian
            transform.position = Vector2.MoveTowards(transform.position, targetPos, enemy.currentMoveSpeed * Time.deltaTime); 
            // Nếu người chơi ở bên trái (targetPos.x < transform.position.x), lật sprite
            if (targetPos.x < transform.position.x)
            {
                spriteRenderer.flipX = true; // Lật sang trái
            }
            else if (targetPos.x > transform.position.x)
            {
                spriteRenderer.flipX = false; // Trở lại bên phải
            }
        }
       
    }
    public void Knockback(Vector2 velocity, float duration)
    {
        //hủy knockback nếu thời gian knockback lớn hơn 0
        if (knockbackDuration > 0)
        {
            return;
        }
        knockbackVelocity = velocity;
        knockbackDuration = duration;
    }
}
