using UnityEngine;

public class Pickup : MonoBehaviour,ICollectible
{
    protected bool hasBeenCollected = false;

    public virtual void Collect()
    {
        hasBeenCollected = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //khi item chạm vào player sẽ biến mất
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
