using UnityEngine;

public class Layers : MonoBehaviour
{
    public GameObject DISTOPIA;
    public GameObject NORMAL;
    public GameObject UTOPIC;

    void Start()
    {
        // Comprovem que les referències estan assignades
        if (DISTOPIA == null || NORMAL == null || UTOPIC == null)
        {
            Debug.LogError("[LAYERS] Falta assignar alguna capa al component!");
            return;
        }

        // Ens assegurem que el GameManager existeix
        if (GameManager.Instance != null)
        {
            // Ens subscrivim al canvi d'estat
            GameManager.Instance.Canvi += Reaccio;

            // Apliquem l'estat inicial
            Reaccio(GameManager.Instance.estatActual);
        }
        else
        {
            Debug.LogError("[LAYERS] GameManager.Instance és NULL al Start!");
        }
    }

    void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.Canvi += Reaccio;
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.Canvi -= Reaccio;
    }

    // Rep l'estat i activa la capa corresponent
    void Reaccio(int estat)
    {
        Debug.Log($"[LAYERS] Reaccio rebuda: estat = {estat}");

        DISTOPIA.SetActive(estat == GameManager.ESTAT_DISTOPIC);
        NORMAL.SetActive(estat == GameManager.ESTAT_NORMAL);
        UTOPIC.SetActive(estat == GameManager.ESTAT_UTOPIC);

        Debug.Log($"[LAYERS] Actius → D:{DISTOPIA.activeSelf} N:{NORMAL.activeSelf} U:{UTOPIC.activeSelf}");
    }
}
