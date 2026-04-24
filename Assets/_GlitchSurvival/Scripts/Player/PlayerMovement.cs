using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    PlayerStats player;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]   
    public Vector2 movement;    
    [HideInInspector]
    public Vector2 lastMovedVector;

    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>(); // Lấy component Rigidbody2D
        lastMovedVector = new Vector2(1, 0f);
    }

    void Update()
    {
        // Nhận input từ bàn phím (WASD hoặc Arrow)
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D hoặc ← →
        movement.y = Input.GetAxisRaw("Vertical");   // W/S hoặc ↑ ↓

        // Chuẩn hóa vector để không bị nhanh hơn khi đi chéo
        movement = movement.normalized;
        if(movement.x != 0)
        {
            lastHorizontalVector = movement.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f); //lấy hướng chuyển động cuối 
        }
        if(movement.y != 0)
        {
            lastVerticalVector = movement.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);
        }
        if (movement.x != 0 && movement.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector); 
        }
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật bằng vật lý
        rb.linearVelocity = movement * player.CurrentMoveSpeed;

    }
}