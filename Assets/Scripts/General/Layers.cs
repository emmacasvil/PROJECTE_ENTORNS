using UnityEngine;
using System;

public class Layers : MonoBehaviour
{
    public GameObject DISTOPIA;
    public GameObject NORMAL;
    public GameObject UTOPIC;

    public int estat = GameManager.Instance.estatActual;

    void Start()
    {
        Debug.Log("Funciona!");
        int distopia = LayerMask.NameToLayer("DISTOPIA"); //6
        int normal = LayerMask.NameToLayer("NORMAL"); //7
        int utopia = LayerMask.NameToLayer("UTOPIA"); //8

        // Inicialitzar l'estat actual al començar
        if (GameManager.Instance != null)
        {
            Reaccio(GameManager.Instance.estatActual);
        }
    }

    void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Canvi += Reaccio;
        }
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Canvi -= Reaccio;
        }
    }

    //Aquí hi ha la lògica de capes que es mostren o no segons l'estat del joc. 
    void Reaccio(int estat)
    {
        
        if (DISTOPIA == null || NORMAL == null || UTOPIC == null)
        {
            Debug.LogWarning("Alguna referència de capa no està assignada!");
            return;
        }

        if (estat == GameManager.ESTAT_DISTOPIC)
        {
            DISTOPIA.SetActive(true);
            NORMAL.SetActive(false);
            UTOPIC.SetActive(false);
        }
        else if (estat == GameManager.ESTAT_NORMAL)
        {
            DISTOPIA.SetActive(false);
            NORMAL.SetActive(true);
            UTOPIC.SetActive(false);
        }
        else if (estat == GameManager.ESTAT_UTOPIC)
        {
            DISTOPIA.SetActive(false);
            NORMAL.SetActive(false);
            UTOPIC.SetActive(true);
        }
    }
}
