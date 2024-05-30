using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController _controller;
    public PlayerCondition _condition;


    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        _controller = GetComponent<PlayerController>();
        _condition = GetComponent<PlayerCondition>();
    }
}
