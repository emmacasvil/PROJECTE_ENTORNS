using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MiniLightController : MonoBehaviour
{
    public Light2D[] miniLights; //les point Lights tenen un sistema diferent, canvien de INTENSITAT, COLOR i RADI
    
    //definim totes les variables
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
    public float velocitatCanvi = 3f; //velocitat del Lerp

    private float intensitatTarget;
    private float radiusTarget;
    private Color colorTarget;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ValorModificat += ReaccioGradual; //es subscriu a la funció del GameManager i crida la funciño ReaccioGradual que hi ha més avall
        }
        else
        {
            Debug.LogWarning("[MINI LIGHTS] GameManager.Instance és NULL"); //si hi ha algun error salta el debug
        }

        //al inici, 
        intensitatTarget = intensitatBase;
        radiusTarget = radiusBase;
        colorTarget = colorNormal;
    }

    void Update()
    {  //el funcionament del Lerp és el mateix que el de PlayerVelocity (mirar aquell script per entendre-ho millor)
        foreach (var light in miniLights) //per cada light, el script farà el següent:
        {
            if (light == null) continue;

            // Intensitat
            light.intensity = Mathf.Lerp(light.intensity, intensitatTarget, Time.deltaTime * velocitatCanvi);

            // Outer Radius (aquest efecte fa que es vagin difonent i fent més grans les llums)
            light.pointLightOuterRadius = Mathf.Lerp(light.pointLightOuterRadius, radiusTarget, Time.deltaTime * velocitatCanvi);

            // Color
            light.color = Color.Lerp( light.color, colorTarget, Time.deltaTime * velocitatCanvi );
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

        //fem el mateix sistema que vam utilitzar per canviar el color del TileMap i dels Sprites (mirar scripts corresponents per entendre-ho)
        if (t < 0.5f)
        {
            colorTarget = Color.Lerp(colorDistopic, colorNormal, t * 2f);
        }
        else
        {
            colorTarget = Color.Lerp(colorNormal, colorUtopic, (t - 0.5f) * 2f);
        }

        //faig un debug cada vegada que hi ha un canvi d'estat, així mirem que TOT vagi bé
        Debug.Log($"[MINI LIGHTS] valorEstat={valorEstat} → intensitat={intensitatTarget}, radius={radiusTarget}, color={colorTarget}");
    }
}