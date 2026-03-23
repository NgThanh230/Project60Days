using UnityEngine;

public class FireController : WeaponController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(prefab);
        spawnedKnife.transform.position = transform.position;
        spawnedKnife.GetComponent<FireBehavior>().DirectionChecker(playerMovement.lastMovedVector);
    }
}
