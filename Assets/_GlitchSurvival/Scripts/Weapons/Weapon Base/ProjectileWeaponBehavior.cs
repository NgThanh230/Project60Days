using UnityEngine;
//Script dành cho tất cả các vũ khí ném
public class ProjectileWeaponBehavior : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCoolDownDuration;
    protected int currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCoolDownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;
        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0)// left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry < 0)//down
        {
            rotation.z = -90f;
        }
        else if (dirx == 0 && diry > 0)//up
        {
            rotation.z = 90f;
        }
        else if (dirx > 0 && diry > 0)//right up
        {
            rotation.z = 45f;
        }
        else if (dirx > 0 && diry < 0)//right down
        {
            rotation.z = -45f;
        }
        else if (dirx < 0 && diry > 0)// left up
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -45f;
        }
        else if(dirx < 0 && diry < 0)// left down
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 45f;
        }
        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //tham chiếu tới collider của enemy và gây damage bằng TakeDamage()
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);  //sử dụng currentDamage vì vũ khí sẽ mạnh lên chứ không nhận dame mặc định
            ReducePierce();
        }
        else if (collision.CompareTag("Prop"))
        {
            if (collision.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDame(currentDamage);
                ReducePierce();
            }
        }
    }
    void ReducePierce()
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
