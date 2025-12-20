using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TilemapColorController : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    private Coroutine fadeRoutine;

    public Color colorDistopic = Color.red;
    public Color colorNormal = new Color(0.8f, 1f, 0.5f);
    public Color colorUtopic = Color.green;

    public float fadeDuration = 0.5f; // duració del gradient

    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();

        // Fem una còpia del material per no afectar altres tilemaps
        tilemapRenderer.material = new Material(tilemapRenderer.material);

        // Ens subscrivim al nou event que detecta qualsevol canvi de valor
        GameManager.Instance.ValorModificat += OnValorModificat;

        // Color inicial
        UpdateColorInstant(GameManager.Instance.valorEstat);
    }

    void OnValorModificat(float valor)
    {
        Color targetColor = CalculateColor(valor);

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeTo(targetColor));
    }

    Color CalculateColor(float valor)
    {
        // 0 → 7 (distòpic → normal)
        if (valor <= 7f)
        {
            float t = valor / 7f;
            return Color.Lerp(colorDistopic, colorNormal, t);
        }

        // 7 → 14 (normal → utòpic)
        if (valor <= 14f)
        {
            float t = (valor - 7f) / 7f;
            return Color.Lerp(colorNormal, colorUtopic, t);
        }

        // 14 → 20 (utòpic → més brillant)
        float tFinal = (valor - 14f) / 6f;
        return Color.Lerp(colorUtopic, Color.white, tFinal * 0.2f);
    }

    IEnumerator FadeTo(Color target)
    {
        Color startColor = tilemapRenderer.material.color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            tilemapRenderer.material.color =
                Color.Lerp(startColor, target, elapsed / fadeDuration);

            yield return null;
        }

        tilemapRenderer.material.color = target;
    }

    void UpdateColorInstant(float valor)
    {
        tilemapRenderer.material.color = CalculateColor(valor);
    }

    void OnDestroy()
    {
        GameManager.Instance.ValorModificat -= OnValorModificat;
    }
}
