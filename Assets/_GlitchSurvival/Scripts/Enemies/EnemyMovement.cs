using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemieScriptableObject enemyData;
    Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.MoveSpeed * Time.deltaTime); //enemy chạy từ điểm spawn tới người chơi, *time để đồng bộ thời gian
    }
}
