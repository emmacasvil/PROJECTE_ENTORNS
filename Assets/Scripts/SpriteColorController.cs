using UnityEngine;
using System;
using System.Collections;
public class SpriteColorController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Coroutine fadeRoutine;

    public Color colorDistopic = Color.red;
    public Color colorNormal = new Color(255f, 255f, 255f);
    public Color colorUtopic = Color.green;
    public float fadeDuration = 0.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.Instance.ValorModificat += OnValorModificat;
        UpdateColorInstant(GameManager.Instance.valorEstat);
    }

    void OnValorModificat(float valor)
    {
        Color targetColor = CalculateColor(valor);
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(FadeTo(targetColor));
    }

    Color CalculateColor(float valor)
    {
        if (valor <= 7f) return Color.Lerp(colorDistopic, colorNormal, valor / 7f);
        if (valor <= 14f) return Color.Lerp(colorNormal, colorUtopic, (valor - 7f) / 7f);
        return Color.Lerp(colorUtopic, Color.white, ((valor - 14f) / 6f) * 0.2f);
    }

    IEnumerator FadeTo(Color target)
    {
        Color startColor = spriteRenderer.color;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(startColor, target, elapsed / fadeDuration);
            yield return null;
        }
        spriteRenderer.color = target;
    }

    void UpdateColorInstant(float valor)
    {
        spriteRenderer.color = CalculateColor(valor);
    }

    void OnDestroy()
    {
        GameManager.Instance.ValorModificat -= OnValorModificat;
    }
}
