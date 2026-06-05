using UnityEngine;

[CreateAssetMenu (fileName = "WeaponEvolutionBlueprint",menuName = "ScriptableObject/WeaponEvolutionBlueprint")]
public class WeaponEvolutionBlueprint : ScriptableObject
{
    public WeaponScriptableObject baseWeaponData;
    public PassiveItemsScripableObject catalystPassiveItemData;
    public WeaponScriptableObject evolvedWeaponData;
    public GameObject evolvedWeapon;
}
