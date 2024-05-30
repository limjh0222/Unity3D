using UnityEngine;

public enum ItemType
{
    Consumable
}

public enum ConsumableType
{
    Health,
    Buff
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType _type;
    public float _value;
}

[CreateAssetMenu(fileName ="Item", menuName ="New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string _displayName;
    public string _description;
    public ItemType _type;
    public Sprite _icon;
    public GameObject _dropPrefab;

    [Header("Stacking")]
    public bool _canStack;
    public int _maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] _consumables;
}
