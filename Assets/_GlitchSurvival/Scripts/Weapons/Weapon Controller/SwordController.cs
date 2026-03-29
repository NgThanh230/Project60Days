using UnityEngine;

public class SwordController : WeaponController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedSword = Instantiate(weaponData.Prefab);
        spawnedSword.transform.position = transform.position;
        spawnedSword.GetComponent<SwordBehavior>().DirectionChecker(playerMovement.lastMovedVector);
    }
}
