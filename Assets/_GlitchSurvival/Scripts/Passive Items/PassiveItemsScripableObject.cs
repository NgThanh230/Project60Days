using UnityEngine;
[CreateAssetMenu(fileName = "PassiveItemsScripableObject", menuName = "ScriptableObject/Passive Item")]
public class PassiveItemsScripableObject : ScriptableObject
{
    [SerializeField]
    float multiplier;
    public float Multiplier { get => multiplier; private set => multiplier = value; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
