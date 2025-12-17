using UnityEngine;
using System;

public class Particles : MonoBehaviour
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
            /////
        }
        else if (estat == GameManager.ESTAT_NORMAL)
        {
            ////
        }
        else if (estat == GameManager.ESTAT_UTOPIC)
        {
            ////
        }
    }
}
