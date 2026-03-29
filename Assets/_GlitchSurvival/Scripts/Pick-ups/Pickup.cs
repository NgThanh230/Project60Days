using UnityEngine;

public class Pickup : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //khi item chạm vào player sẽ biến mất
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
