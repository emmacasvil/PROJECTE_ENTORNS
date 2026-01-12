using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour
{
    // STATES
    public const int Sana = 0;
    public const int Seca = 1;
    public const int Morta = 2;
    public const int Gone = 3;

    [Header("Type (identity)")]
    public FlowerType flowerType;

    [Header("State")]
    private int estatActual = Sana;

    [Header("Timing")]
    [SerializeField] private float tempsSana = 10f;
    [SerializeField] private float tempsSeca = 5f;
    [SerializeField] private float tempsMorta = 5f;

    [Header("UtopicDance")]
    [SerializeField] private float tempsFlip = 0.25f;

    private SpriteRenderer spriteRenderer;
    private Coroutine rutinaTemps;
    private Coroutine rutinaDance;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ActualitzarVisual();
        rutinaTemps = StartCoroutine(ControlTemps());

        GameManager.Instance.Canvi += OnWorldStateChanged;
    }

    IEnumerator ControlTemps()
    {
        yield return new WaitForSeconds(tempsSana);
        CanviarEstat(Seca);

        yield return new WaitForSeconds(tempsSeca);
        CanviarEstat(Morta);

        yield return new WaitForSeconds(tempsMorta);
        CanviarEstat(Gone);
    }

    IEnumerator Dance()
    {
        while (true) 
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            yield return new WaitForSeconds(tempsFlip);
        }
    }

    public void SetType(FlowerType type)
    {
        flowerType = type;
        ActualitzarVisual();
    }

    public void CanviarEstat(int nouEstat)
    {
        if (nouEstat < Sana || nouEstat > Gone) return;

        estatActual = nouEstat;
        ActualitzarVisual();
        UpdateDanceState();

        Debug.Log($"{name} canviant a estat {estatActual}");

        if (estatActual == Gone)
            DestroyFlower();
    }

    public void AtendreFlor()
    {
        if (estatActual == Morta)
        {
            Debug.Log("Intentant atendre una flor morta");
            return;
        }

        if (rutinaTemps != null)
            StopCoroutine(rutinaTemps);

        CanviarEstat(Sana);
        rutinaTemps = StartCoroutine(ControlTemps());

        GameManager.Instance.Modifier(1f);
    }

    void ActualitzarVisual()
    {
        if (flowerType == null || spriteRenderer == null) return;

        switch (estatActual)
        {
            case Sana:
                spriteRenderer.sprite = flowerType.sanaSprite;
                break;
            case Seca:
                spriteRenderer.sprite = flowerType.secaSprite;
                break;
            case Morta:
                spriteRenderer.sprite = flowerType.mortaSprite;
                break;
        }
    }

    void StartDance()
    {
        if (rutinaDance != null) return;
        rutinaDance = StartCoroutine(Dance());
    }

    void StopDance()
    {
        if (rutinaDance != null)
        {
            StopCoroutine(rutinaDance);
            rutinaDance = null;
        }
        spriteRenderer.flipX = false;
    }

    void OnWorldStateChanged(int nouEstat)
    {
        UpdateDanceState();
    }

    void UpdateDanceState()
    {
        bool isSana = estatActual == Sana;
        bool isUtopic = GameManager.Instance.estatActual == GameManager.ESTAT_UTOPIC;

        if (isSana && isUtopic)
            StartDance();
        else
            StopDance();
    }

    void DestroyFlower()
    {
        GameManager.Instance.Modifier(-1f);
        Destroy(gameObject, 3f);

        if (GameManager.Instance != null)
            GameManager.Instance.Canvi -= OnWorldStateChanged;
    }
}