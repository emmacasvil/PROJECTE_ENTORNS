using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TilemapColorController : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer; //crearem una variable privada, serveix per llegir el renderer del mapa
    private Coroutine fadeRoutine; //una corutina per fer el canvia gradual

    //aquests són els colors segons l'estat del joc, són visibles al inspector
    public Color colorDistopic = Color.red;
    public Color colorNormal = new Color(0.8f, 1f, 0.5f);
    public Color colorUtopic = Color.green;

    public float fadeDuration = 0.5f; // duració del gradient

    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>(); //llegim el renderer

        // Fem una còpia perquè cada tilemap tingui el seu propi material i no es modifiquin tots de cop
        tilemapRenderer.material = new Material(tilemapRenderer.material);

        // Ens subscrivim al nou event del GameManager que detecta qualsevol canvi de valor
        GameManager.Instance.ValorModificat += OnValorModificat; // a la mínima que puji o baixi el valor del estat cridarem la següent funció

        // Color inicial
        UpdateColorInstant(GameManager.Instance.valorEstat);
    }

    void OnValorModificat(float valor)
    {
        Color targetColor = CalculateColor(valor); //li assignarem el següent color calculat amb la funció CalculateColor()

        if (fadeRoutine != null) //si hi havia un fade fent-se atura la corutina (així evitem problemes i possibles bugs visuals)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeTo(targetColor)); //cridem la corutina i comencem la transició suau entre colors
    }

    Color CalculateColor(float valor)
    { //al igual que el AudioManager, fem servir una variable auxiliar t, per controlar la transició (mirar script que està explicat allà)
        
        //els següents if van mirant quin és l'estat i cap a on s'ha de transicionar (repeteixo que és el mateix funcionament que amb el AudioManager)
        // 0 → 7 (distòpic → normal)
        if (valor <= 7f)
        {
            float t = valor / 7f;
            return Color.Lerp(colorDistopic, colorNormal, t); //utilitzem la funció lerp vista a classe per fusionar els colors
        }

        // 7 → 14 (normal → utòpic)
        if (valor <= 14f)
        {
            float t = (valor - 7f) / 7f;
            return Color.Lerp(colorNormal, colorUtopic, t);
        }

        // 14 → 20 (utòpic → més brillant)
        float tFinal = (valor - 14f) / 6f;
        return Color.Lerp(colorUtopic, Color.white, tFinal * 0.2f); //afegeixo una petita brillantor quan arriba al final (valor estat = 20)
    }

    IEnumerator FadeTo(Color target) //aquesta es la corutina és la que ens fa que el gradient sigui suau i no un canvi brusc
    {
        Color startColor = tilemapRenderer.material.color; //guarda el color inicial i el temps transcorregut.
        float elapsed = 0f;

        while (elapsed < fadeDuration)  //mentre que el temps transcorregut sigui més petit que la duració del gradient, farà el següent loop:
        {
            elapsed += Time.deltaTime; //cada frame incrementem el temps
            tilemapRenderer.material.color = Color.Lerp(startColor, target, elapsed / fadeDuration); //calculem un color entre mig del color inicial i del final

            yield return null;
        }

        tilemapRenderer.material.color = target; //ens assegurem d'actualitzar el color final del estat
    }

    void UpdateColorInstant(float valor) //al principi del joc, NO fa cap fade, posa el color distòpic tal qual, així evitem problemes
    {
        tilemapRenderer.material.color = CalculateColor(valor);
    }

    void OnDestroy() //es desubscriu de l’event per evitar errors.
    {
        GameManager.Instance.ValorModificat -= OnValorModificat;
    }
}
