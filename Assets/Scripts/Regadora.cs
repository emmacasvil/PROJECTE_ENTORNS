using UnityEngine;

public class Regadora : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Regar();
        }
    }

    void Regar()
    {

    }

    void FlorRegada()
    {

    }
}


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
//EL CODI HA DE FER 
//1 - LLEGIR EL INPUT DEL JUGADOR (TECLA F)
//2 - ENVIAR-LI UN MISSATGE A LA FLOR DE HEY! HE ESTAT REGADA ;)
//3 - Llegir l'estat del joc amb el GameManager