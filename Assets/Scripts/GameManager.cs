using UnityEngine;
using System; 

public class GameManager : MonoBehaviour
{

    //static vol dir que només hi ha una còpia de l'objecte. Instance és una referència global al GameManager. 
    public static GameManager Instance;

    //Crea un event; una llista de funcions que s'executen quan hi ha un canvi. 
    public event Action<int> Canvi; 

    //Declaració dels estats
    public const int ESTAT_DISTOPIC = 0;
    public const int ESTAT_NORMAL = 1;
    public const int ESTAT_UTOPIC = 2;

    // Estat actual del joc, es va actualitzant durant la partida. 
    public int estatActual = ESTAT_DISTOPIC;

    //Aquí s'indica que el Game Manager serà la instància global del joc. S'executa quan es crea l'objecte abans de l'start. 
    private void Awake()
    {
        Instance = this; 
    }

    //Funció per canviar d'estat. 
    public void CanviarEstat(int nouEstat) //Es passa el valor del nou estat per paràmetre. 
    {
        if(nouEstat < 0 || nouEstat > 2) //Aquí es comprova si l'estat és un dels estats establerts i en cas contrari surt de la funció. 
        {
            return; 
        }
        
        estatActual = nouEstat; //Canvi d'estat
        Canvi?.Invoke(estatActual); //Avisar a totes les funcions que tenen listeners
        Debug.Log("Estat canviant a: " + estatActual); //Mostra el missatge a la consola
    }

}
