using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Weapon Stats")]
    public GameObject prefab;
    public float damage;
    public float speed;
    public float cooldownDuration;
    float currentCooldown;
    public int pierce;

    protected PlayerMovement playerMovement;
    protected virtual void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        currentCooldown = cooldownDuration; 
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }
    protected virtual void Attack()
    {
        currentCooldown = cooldownDuration;
    }
}
