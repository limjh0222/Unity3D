using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition _health;

    private void Start()
    {
       CharacterManager.Instance.Player._condition._uiCondition = this;
    }
}
