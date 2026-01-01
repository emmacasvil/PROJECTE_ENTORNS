using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MiniLightController : MonoBehaviour
{
    public Light2D[] miniLights;

    [Header("Intensitat")]
    public float intensitatBase = 6f;
    public float intensitatIncrement = 0.4f;

    [Header("Outer Radius (Falloff real de Point Light)")]
    public float radiusBase = 1f;
    public float radiusIncrement = 0.3f; 

    [Header("Colors segons estat")]
    public Color colorDistopic = Color.white;
    public Color colorNormal = Color.blue;
    public Color colorUtopic = Color.yellow;

    [Header("Velocitat de transició")]
    public float velocitatCanvi = 3f;

    private float intensitatTarget;
    private float radiusTarget;
    private Color colorTarget;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ValorModificat += ReaccioGradual;
        }
        else
        {
            Debug.LogWarning("[MINI LIGHTS] GameManager.Instance és NULL");
        }

        intensitatTarget = intensitatBase;
        radiusTarget = radiusBase;
        colorTarget = colorNormal;
    }

    void Update()
    {
        foreach (var light in miniLights)
        {
            if (light == null) continue;

            // Intensitat
            light.intensity = Mathf.Lerp(
                light.intensity,
                intensitatTarget,
                Time.deltaTime * velocitatCanvi
            );

            // Outer Radius (falloff real de Point Light)
            light.pointLightOuterRadius = Mathf.Lerp(
                light.pointLightOuterRadius,
                radiusTarget,
                Time.deltaTime * velocitatCanvi
            );

            // Color
            light.color = Color.Lerp(
                light.color,
                colorTarget,
                Time.deltaTime * velocitatCanvi
            );
        }
    }

    public void ReaccioGradual(float valorEstat)
    {
        // Intensitat contínua
        intensitatTarget = intensitatBase + valorEstat * intensitatIncrement;

        // Outer Radius contínuament més gran
        radiusTarget = radiusBase + valorEstat * radiusIncrement;

        // COLOR CONTINU (0–20 → 0–1)
        float t = valorEstat / 20f;

        if (t < 0.5f)
        {
            colorTarget = Color.Lerp(colorDistopic, colorNormal, t * 2f);
        }
        else
        {
            colorTarget = Color.Lerp(colorNormal, colorUtopic, (t - 0.5f) * 2f);
        }

        Debug.Log($"[MINI LIGHTS] valorEstat={valorEstat} → intensitat={intensitatTarget}, radius={radiusTarget}, color={colorTarget}");
    }
}