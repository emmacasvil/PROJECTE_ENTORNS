using UnityEngine;
using System;

public class FlowerState : MonoBehaviour
{
    // Event: avisa quan canvia l’estat de la flor
    public event Action<FlowerState, int> CanviFlor;

    // Estats
    public const int sana = 0;
    public const int seca = 1;
    public const int morta = 2;

    public int estatActual = sana;

    // Temps abans de canviar d’estat
    [SerializeField] private float tempsSana = 10f;
    [SerializeField] private float tempsSeca = 5f;

    private Coroutine rutinaTemps; //Guarda la coroutine que controla el temps, serveix per iniciar, aturar o reiniciar la funció. 

    void Start()
    {
        // Quan neix, comença el compte enrere
        rutinaTemps = StartCoroutine(ControlTemps());
    }

    IEnumerator ControlTemps() //Coroutine que permet esperar sense bloquejar el joc. Fa de temporitzador. 
    {
        yield return new WaitForSeconds(tempsSana);
        CanviarEstat(seca);

        yield return new WaitForSeconds(tempsSeca);
        CanviarEstat(morta);
    }

    public void CanviarEstat(int nouEstat)  //Canvia l'estat de la flor NO EL DEL JOC. 
    {
        if (nouEstat < 0 || nouEstat > 2) return; //Evita estats inexistents

        estatActual = nouEstat; //Actualitza l'estat de la flor. 

        // Avisar als listeners
        CanviFlor?.Invoke(this, estatActual);

        Debug.Log("Flor canviant a estat: " + estatActual);

        if (estatActual == morta) //Si la flor està morta, s'autodestrueix. 
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

    //Destructor
    private void DestroyFlower()
    {
        Destroy(gameObject, 3f);
    }
}
