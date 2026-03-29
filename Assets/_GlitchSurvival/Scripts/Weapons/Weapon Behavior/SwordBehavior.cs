using UnityEngine;

public class SwordBehavior : ProjectileWeaponBehavior
{
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * currentSpeed * Time.deltaTime; //Set chuyển động cho vũ khí
    }

}
