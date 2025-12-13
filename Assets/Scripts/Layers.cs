/*
using UnityEngine;
using System

public class Layers : MonoBehaviour
{
    //Retorna el número intern de la capa amb el nom
    int distopia = LayerMask.NameToLayer("DISTOPIA"); //6
    int normal = LayerMask.NameToLayer("NORMAL"); //7
    int utopia = LayerMask.NameToLayer("UTOPIA"); //8

    void Start()
    {
        Debug.Log("Funciona!");
    }

    void OnEnable()
    {
        GameManager.Instance.Canvi += Reaccio;
    }

    void OnDisable()
    {
        GameManager.Instance.Canvi -= Reaccio;
    }

    //Aquí hi ha la lògica de capes que es mostren o no segons l'estat del joc. 
    void Reaccio(int estat)
    {
        if (estat == GameManager.ESTAT_DISTOPIC)
        {
            objecteDISTOPIA.SetActive(true);
            objecteNORMAL.SetActive(false);
            objecteUTOPIC.SetActive(false);
        }
        else if (estat == GameManager.ESTAT_NORMAL)
        {
            objecteDISTOPIA.SetActive(false);
            objecteNORMAL.SetActive(true);
            objecteUTOPIC.SetActive(false);
        }
        else if (estat == GameManager.ESTAT_UTOPIC)
        {
            objecteDISTOPIA.SetActive(false);
            objecteNORMAL.SetActive(false);
            objecteUTOPIC.SetActive(true);
        }
    }

}
*/