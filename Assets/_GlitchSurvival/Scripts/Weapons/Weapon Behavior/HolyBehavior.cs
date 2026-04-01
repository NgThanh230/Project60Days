using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HolyBehavior : MeleeWeaponHavior
{
    List<GameObject> markedEnemies;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //tham chiếu tới collider của enemy và gây damage bằng TakeDamage()
        if (collision.CompareTag("Enemy") && !markedEnemies.Contains(collision.gameObject))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());  //sử dụng currentDamage vì vũ khí sẽ mạnh lên chứ không nhận dame mặc định
            markedEnemies.Add(collision.gameObject); //đánh dấu kẻ địch để không nhận damage lần thứ 2 
        }
        else if (collision.CompareTag("Prop"))
        {
            if (collision.gameObject.TryGetComponent(out BreakableProps breakable) && !markedEnemies.Contains(collision.gameObject))
            {
                breakable.TakeDame(GetCurrentDamage());
                markedEnemies.Add(collision.gameObject);
            }
        }
    }
}
