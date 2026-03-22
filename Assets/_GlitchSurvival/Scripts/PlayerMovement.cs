using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;   // Tốc độ di chuyển
    private Rigidbody2D rb;     
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]   // Tham chiếu tới Rigidbody2D
    public Vector2 movement;      // Vector lưu input

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy component Rigidbody2D
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
        }
        if(movement.y != 0)
        {
            lastVerticalVector = movement.y;
        }
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật bằng vật lý
        rb.linearVelocity = movement * moveSpeed;
    }
}