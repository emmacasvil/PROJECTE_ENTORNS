using UnityEngine;

public class Regadora : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GetComponent<PLAYER>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Input.GetKeyDown(F))
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
//EL CODI HA DE FER 
//1 - LLEGIR EL INPUT DEL JUGADOR (TECLA F)
//2 - ENVIAR-LI UN MISSATGE A LA FLOR DE HEY! HE ESTAT REGADA ;)

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