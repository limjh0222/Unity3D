using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData _data;

    public string GetInteractPrompt()
    {
        string str = $"{_data._displayName}\n{_data._description}";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player._itemData = _data;
        CharacterManager.Instance.Player._addItem?.Invoke();
        Destroy(gameObject);
    }
}
