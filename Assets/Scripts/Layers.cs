using UnityEngine;
using System;

public class Layers : MonoBehaviour
{
    public GameObject DISTOPIA;
    public GameObject NORMAL;
    public GameObject UTOPIC;

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

<<<<<<< Updated upstream
<<<<<<< Updated upstream

}

=======
}
>>>>>>> Stashed changes
=======
}
>>>>>>> Stashed changes
