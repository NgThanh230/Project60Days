using UnityEngine;

public class FireBehavior : ProjectileWeaponBehavior
{
    FireController fireController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        fireController = FindAnyObjectByType<FireController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * fireController.speed * Time.deltaTime; //Set chuyển động cho vũ khí
    }
}
