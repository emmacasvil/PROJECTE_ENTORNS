using UnityEngine;
using System;

/// AQUEST CODI CONTROLA L'ESTAT DE CADA FLOR INDIVIDUALMENT PER SABER SI ESTÀ BÉ, NECESSITA AIGUA O SI ESTÀ MORTA. ///

public class FlowerState : MonoBehaviour
{
    //Crea un event per avisar del canvi d'estat;
    public event Action<FlowerState, int> CanviFlor;

    //Declaracio dels estats
    public const int sana = 0;
    public const int seca = 1;
    public const int morta = 2;

    // Estat inicial de la flor; si es mor es destrueix l'objecte. 
    public int estatActual = sana;

    // Temps abans de canviar d’estat
    [SerializeField] private float tempsSana = 10f;
    [SerializeField] private float tempsSeca = 5f;

    private Coroutine rutinaTemps;

    void Start()
    {
        // Quan neix, comença el compte enrere
        rutinaTemps = StartCoroutine(ControlTemps());
    }

    IEnumerator ControlTemps()
    {
        yield return new WaitForSeconds(tempsSana);
        CanviarEstat(seca);

        yield return new WaitForSeconds(tempsSeca);
        CanviarEstat(morta);
    }

    public void CanviarEstat(int nouEstat)
    {
        if (nouEstat < 0 || nouEstat > 2) return;

        estatActual = nouEstat;

        // Avisar als listeners
        CanviFlor?.Invoke(this, estatActual);

        Debug.Log("Flor canviant a estat: " + estatActual);

        if (estatActual == morta)
        {
            DestroyFlower();
        }
    }

    // Acció del jugador (regar, podar, etc.)
    public void AtendreFlor()
    {
        if (estatActual == morta) return;

        // Reiniciem el temporitzador
        if (rutinaTemps != null)
            StopCoroutine(rutinaTemps);

        CanviarEstat(sana);
        rutinaTemps = StartCoroutine(ControlTemps());
    }

    private void DestroyFlower()
    {
        Destroy(gameObject, 3f);
    }
}
}
