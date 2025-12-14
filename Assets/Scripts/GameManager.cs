using UnityEngine;
using System; 

/// AQUEST CODI ES EL QUE CONTROLA ELS ESTATS GENERALS DEL JOC (UTOPIC, DISTOPIC I NORMAL) ///

public class GameManager : MonoBehaviour
{

    //static vol dir que nomes hi ha una copia de l'objecte. Instance es una referencia global al GameManager. 
    public static GameManager Instance;

    //Crea un event; una llista de funcions que s'executen quan hi ha un canvi. 
    public event Action<int> Canvi; 

    //Declaracio dels estats
    public const int ESTAT_DISTOPIC = 0;
    public const int ESTAT_NORMAL = 1;
    public const int ESTAT_UTOPIC = 2;

    // Estat inicial del joc, es va actualitzant durant la partida. 
    public int estatActual = ESTAT_DISTOPIC;

    //Aqui s'indica que el Game Manager sera la instancia global del joc. S'executa quan es crea l'objecte abans de l'start. 
    private void Awake()
    {
        Instance = this; 
    }

    //Funcio per canviar d'estat. 
    public void CanviarEstat(int nouEstat) //Es passa el valor del nou estat per parametre. 
    {
        if(nouEstat < 0 || nouEstat > 2) //Aqui es comprova si l'estat es un dels estats establerts i en cas contrari surt de la funcio. 
        {
            return; 
        }
        
        estatActual = nouEstat; //Canvi d'estat
        Canvi?.Invoke(estatActual); //Avisar a totes les funcions que tenen listeners
        Debug.Log("Estat canviant a: " + estatActual); //Mostra el missatge a la consola
    }

}
