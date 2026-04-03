using UnityEngine;
[CreateAssetMenu(fileName = "PassiveItemsScripableObject", menuName = "ScriptableObject/Passive Item")]
public class PassiveItemsScripableObject : ScriptableObject
{
    [SerializeField]
    float multiplier;
    public float Multiplier { get => multiplier; private set => multiplier = value; }
    [SerializeField]
    int level;
    public int Level { get => level; private set => level = value; }
    [SerializeField]
    GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }
}
