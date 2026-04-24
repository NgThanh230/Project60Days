using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed;
    private void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        playerCollector.radius = player.CurrentMagnet;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //khai báo rig cho item
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            //lấy hướng của item tới người chơi (vị trí người chơi - vị trí item).normalized set vector thành 1 để item theo mọi vị trí đều có vận tốc hút như nhau
            Vector2 forceDirection = (transform.position - collision.transform.position).normalized;
            //add force cho item * với pullSpeed.
            rb.AddForce(forceDirection * pullSpeed);
            collectible.Collect();
        }
    }
}
