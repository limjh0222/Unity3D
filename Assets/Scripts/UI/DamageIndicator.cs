using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image _image;
    public float flashSpeed;

    private Coroutine _coroutine;

    private void Start()
    {
        CharacterManager.Instance.Player._condition.onTakeDamge += Flash;
    }

    private void Flash()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _image.enabled = true;
        _coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float starAlpha = 0.3f;
        float a = starAlpha;
        Color color = _image.color;

        while (a > 0.0f)
        {
            a -= (starAlpha / flashSpeed) * Time.deltaTime;
            color.a = a;
            _image.color = color;
            yield return null;
        }
        _image.enabled = false;
    }
}
