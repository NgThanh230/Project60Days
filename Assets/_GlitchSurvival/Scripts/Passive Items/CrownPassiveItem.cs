using UnityEngine;

public class CrownPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.currentMight *= 1 + passiveItemData.Multiplier / 100f;
    }
}
