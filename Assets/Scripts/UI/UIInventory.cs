using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] _slots;

    public GameObject _inventoryWindow;
    public Transform _slotPanel;
    public Transform _dropPosition;

    [Header("Selected Item")]
    private ItemSlot _selectedItem;
    private int _selectedItemIndex;
    public TextMeshProUGUI _selectedItemName;
    public TextMeshProUGUI _selectedItemDescription;
    public TextMeshProUGUI _selectedItemStatName;
    public TextMeshProUGUI _rselectedItemStatValue;
    public GameObject _useButton;
    public GameObject _dropButton;

    private PlayerController _controller;
    private PlayerCondition _condition;

    void Start()
    {
        _controller = CharacterManager.Instance.Player._controller;
        _condition = CharacterManager.Instance.Player._condition;
        _dropPosition = CharacterManager.Instance.Player._dropPosition;

        _controller._inventory += Toggle;
        CharacterManager.Instance.Player._addItem += AddItem;

        _inventoryWindow.SetActive(false);
        _slots = new ItemSlot[_slotPanel.childCount];

        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i] = _slotPanel.GetChild(i).GetComponent<ItemSlot>();
            _slots[i]._index = i;
            _slots[i]._inventory = this;
            _slots[i].Clear();
        }

        ClearSelectedItemWindow();
    }

    public void Toggle()
    {
        if (_inventoryWindow.activeInHierarchy)
        {
            _inventoryWindow.SetActive(false);
        }
        else
        {
            _inventoryWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return _inventoryWindow.activeInHierarchy;
    }

    public void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player._itemData;

        if (data._canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot._quantity++;
                UpdateUI();
                CharacterManager.Instance.Player._itemData = null;
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot._item = data;
            emptySlot._quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player._itemData = null;
            return;
        }

        ThrowItem(data);
        CharacterManager.Instance.Player._itemData = null;
    }

    public void ThrowItem(ItemData data)
    {
        Instantiate(data._dropPrefab, _dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    public void UpdateUI()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i]._item != null)
            {
                _slots[i].Set();
            }
            else
            {
                _slots[i].Clear();
            }
        }
    }

    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i]._item == data && _slots[i]._quantity < data._maxStackAmount)
            {
                return _slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i]._item == null)
            {
                return _slots[i];
            }
        }
        return null;
    }

    public void SelectItem(int index)
    {
        if (_slots[index]._item == null) return;

        _selectedItem = _slots[index];
        _selectedItemIndex = index;

        _selectedItemName.text = _selectedItem._item._displayName;
        _selectedItemDescription.text = _selectedItem._item._description;

        _selectedItemStatName.text = string.Empty;
        _rselectedItemStatValue.text = string.Empty;

        for (int i = 0; i < _selectedItem._item._consumables.Length; i++)
        {
            _selectedItemStatName.text += _selectedItem._item._consumables[i]._type.ToString() + "\n";
            _rselectedItemStatValue.text += _selectedItem._item._consumables[i]._value.ToString() + "\n";
        }

        _useButton.SetActive(_selectedItem._item._type == ItemType.Consumable);
        _dropButton.SetActive(true);
    }

    void ClearSelectedItemWindow()
    {
        _selectedItem = null;

        _selectedItemName.text = string.Empty;
        _selectedItemDescription.text = string.Empty;
        _selectedItemStatName.text = string.Empty;
        _rselectedItemStatValue.text = string.Empty;

        _useButton.SetActive(false);
        _dropButton.SetActive(false);
    }

    public void OnUseButton()
    {
        if (_selectedItem._item._type == ItemType.Consumable)
        {
            for (int i = 0; i < _selectedItem._item._consumables.Length; i++)
            {
                switch (_selectedItem._item._consumables[i]._type)
                {
                    case ConsumableType.Health:
                        _condition.Heal(_selectedItem._item._consumables[i]._value); break;
                    //case ConsumableType.Stat:
                        //_condition.Buff(_selectedItem._item._consumables[i]._value); break;
                }
            }
            RemoveSelctedItem();
        }
    }

    public void OnDropButton()
    {
        ThrowItem(_selectedItem._item);
        RemoveSelctedItem();
    }

    void RemoveSelctedItem()
    {
        _selectedItem._quantity--;

        if (_selectedItem._quantity <= 0)
        {
            _selectedItem._item = null;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }

    public bool HasItem(ItemData item, int quantity)
    {
        return false;
    }
}