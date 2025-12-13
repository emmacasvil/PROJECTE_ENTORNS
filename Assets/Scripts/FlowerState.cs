using UnityEngine;
using System;

/// AQUEST CODI CONTROLA L'ESTAT DE CADA FLOR INDIVIDUALMENT PER SABER SI ESTÀ BÉ, NECESSITA AIGUA O SI ESTÀ MORTA. ///

public class FlowerState : MonoBehaviour
{
    //Crea un event;
    public event Action<int> CanviFlor;

    //Declaracio dels estats
    public const int sana = 0;
    public const int seca = 1;
    public const int morta = 2;

    // Estat inicial de la flor; si es mor es destrueix l'objecte. 
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

        if (nouEstat == 2)
        {
            DestroyFlower(); 
        }

        estatActual = nouEstat; //Canvi d'estat
        Debug.Log("Estat canviant a: " + estatActual); //Mostra el missatge a la consola
    }

    private void DestroyFlower()
    {
        float lifetime = 3f; 
        Destroy(gameObject, lifetime); 
    }
}
