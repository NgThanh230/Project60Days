using UnityEngine;
//Script dành cho tất cả các vũ khí cân chiến
public class MeleeWeaponController : MonoBehaviour
{
    public float destroyAfterSeconds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }
}
