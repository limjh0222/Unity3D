using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float _curValue;
    public float _maxValue;
    public float _startValue;
    public Image _uiBar;

    private void Start()
    {
        _curValue = _startValue;
    }

    private void Update()
    {
        _uiBar.fillAmount = GetPercentage();
    }

    public float GetPercentage()
    {
        return _curValue / _maxValue;
    }

    public void Add(float amount)
    {
        _curValue = Mathf.Min(_curValue + amount, _maxValue);
    }

    public void Sub(float amount)
    {
        _curValue = Mathf.Max(_curValue - amount, 0f);
    }
}
