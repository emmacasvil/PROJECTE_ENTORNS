using UnityEngine;

public class GameManager : MonoBehaviour
{

  
    public const int ESTAT_DISTOPIC = 0;
    public const int ESTAT_NORMAL = 1;
    public const int ESTAT_UTOPIC = 2;

    // Estat actual del joc
    public int estatActual = ESTAT_DISTOPIC;

    // Funció per canviar d'estat
    public void CanviarEstat(int nouEstat)
    {
        estatActual = nouEstat;
        Debug.Log("Nou estat: " + estatActual);
    }

    void Update()
    {
        if (GameManager.estatActual == GameManager.ESTAT_DISTOPIC)
        {
            return ESTAT_DISTOPIC;
        }
        else if (GameManager.estatActual == GameManager.ESTAT_NORMAL)
        {
            return ESTAT_NORMAL;
        }
        else if (GameManager.estatActual == GameManager.ESTAT_UTOPIC)
        {
            return ESTAT_UTOPIC;
        }
    }
}
