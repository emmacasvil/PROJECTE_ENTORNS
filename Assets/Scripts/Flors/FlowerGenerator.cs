using UnityEngine;
using System;

///AQUEST CODI ES UN EXEMPLE D'ESTRUCTURA, NO TOCAR (copiar i enganxar per tenir l'estructura feta okey) ///
public class FlowerGenerator : MonoBehaviour
{
    void OnEnable()
    {
        GameManager.Instance.Canvi += Reaccio;
    }

    void OnDisable()
    {
        GameManager.Instance.Canvi -= Reaccio;
    }

    void Reaccio(int estat)
    {
        if (estat == GameManager.ESTAT_DISTOPIC)
        {
           
        }
        else if (estat == GameManager.ESTAT_NORMAL)
        {
            
        }
        else if (estat == GameManager.ESTAT_UTOPIC)
        {
            
        }
    }
}
