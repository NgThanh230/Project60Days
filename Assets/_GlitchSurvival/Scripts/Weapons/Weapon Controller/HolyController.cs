using UnityEngine;

public class HolyController : WeaponController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedHoly = Instantiate(prefab);
        spawnedHoly.transform.parent = transform; //gắn liền với nhân vật
        spawnedHoly.transform.position = transform.position;//spawn tại nhân vật
        
    }
}
