using UnityEngine;
using System;

/// AQUEST CODI CONTROLA L'ESTAT DE CADA FLOR INDIVIDUALMENT PER SABER SI ESTÀ BÉ, NECESSITA AIGUA O SI ESTÀ MORTA. ///

public class FlowerState : MonoBehaviour
{
    //static vol dir que nomes hi ha una copia de l'objecte. Instance es una referencia global al GameManager. 
    public static GameManager Instance;

    //Crea un event;
    public event Action<int> CanviFlor;

    //Declaracio dels estats
    public const int sana = 0;
    public const int seca = 1;
    public const int morta = 2;

    // Estat actual de la flor; si es mor es destrueix l'objecte. 
    public int estatActual = sana;

    //Aqui s'indica que el Game Manager sera la instancia global del joc. S'executa quan es crea l'objecte abans de l'start. 
    public void NovaFlor()
    {
        Instance = this;
    }

    //Funcio per canviar d'estat. 
    public void CanviarEstat(int nouEstat) //Es passa el valor del nou estat per parametre. 
    {
        if (nouEstat < 0 || nouEstat > 2) //Aqui es comprova si l'estat es un dels estats establerts i en cas contrari surt de la funcio. 
        {
            return;
        }

        estatActual = nouEstat; //Canvi d'estat
        Canvi?.Invoke(estatActual); //Avisar a totes les funcions que tenen listeners
        Debug.Log("Estat canviant a: " + estatActual); //Mostra el missatge a la consola
    }
}
