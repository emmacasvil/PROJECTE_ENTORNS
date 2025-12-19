using UnityEngine;
using System;
using System.Collections;

public class Tilemap : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Coroutine _fadeRoutine;

    private void OnEnable()
    {
        GameManager.Instance.Canvi += ColorFade;
    }

    private void OnDisable()
    {
        GameManager.Instance.Canvi -= ColorFade;
    }

    void ColorFade(int estat)
    {
        Color targetColor = Color.white; //color per defecte és blanc (neutre)

        //canviem a codis hex perquè quedi més maco que amb els per defecte
        switch (estat)
        {
            case 0:
                ColorUtility.TryParseHtmlString("9D7F3F", out targetColor);
                break;
            case 1:
                ColorUtility.TryParseHtmlString("FFFFFF", out targetColor);
                break;
            case 2:
                ColorUtility.TryParseHtmlString("C1FCE2", out targetColor);
                break;
        }

        //comença la transició al seguent color
        if (_fadeRoutine != null) StopCoroutine(_fadeRoutine);
        _fadeRoutine = StartCoroutine(FadeTo(targetColor));
    }

    IEnumerator FadeTo(Color target)
    {
        float duration = 1.0f; //duració del fade
        float elapsed = 0f;
        Color startColor = _renderer.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _renderer.color = Color.Lerp(startColor, target, elapsed / duration);
            yield return null; //espera al següent frame
        }
        _renderer.color = target;
    }

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    //AQUEST CODI NO FUNCIONARÀ BÉ FINS QUE NO S'ARREGLI EL MODIFIER DEL GAMEMANAGER (quan es cridi hauria de canviar el color)
}
