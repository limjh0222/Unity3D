using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData _item;

    public UIInventory _inventory;
    public Button _button;
    public Image _icon;
    public TextMeshProUGUI _quatityText;
    private Outline _outline;

    public int _index;
    public int _quantity;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    public void Set()
    {
        _icon.gameObject.SetActive(true);
        _icon.sprite = _item._icon;
        _quatityText.text = _quantity > 1 ? _quantity.ToString() : string.Empty;
    }

    public void Clear()
    {
        _item = null;
        _icon.gameObject.SetActive(false);
        _quatityText.text = string.Empty;
    }

    public void OnClickButton()
    {
        _inventory.SelectItem(_index);
    }
}