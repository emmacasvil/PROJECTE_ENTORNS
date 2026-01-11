using UnityEngine;                        
using System.Collections.Generic;          

public class FlowerGenerator : MonoBehaviour 
{
    [SerializeField] private GameObject flowerPrefab; // Prefab de la flor assignable des de l'Inspector.
    [SerializeField] private Transform spawnArea; // Definició del punt d'aparició.
    [SerializeField] private int maxFlors = 15; // Nombre màxim de flors que poden existir alhora.

    private List<GameObject> florsActives = new List<GameObject>(); // Llista per guardar referències a les flors creades
    private int contadorFlors = 0; // Comptador de flors vives/actives a l'inici de la partida. 

    private void Start()
    {
        contadorFlors = 0; //El comptador comença en 0
        florsActives.Clear();
        //Generem les dues flors inicials
        CrearFlor();
        CrearFlor();
    }

    private void OnEnable()                         
    {
        GameManager.Instance.Canvi += Reaccio; // S'inscriu a l'esdeveniment "Canvi" del GameManager per rebre notificacions d'estat
    }

    private void OnDisable()                       
    {
        GameManager.Instance.Canvi -= Reaccio; // Es desinscriu de l'esdeveniment per evitar crides quan no toca i fugues de memòria
    }

    // Canvi d'estat al game manager
    void Reaccio(int estat)                         
    {
        StopAllCoroutines(); // Atura qualsevol coroutine de generació en marxa per no solapar ritmes de spawn

        if (estat == GameManager.ESTAT_DISTOPIC)    
        {
            StartCoroutine(GenerarFlorsCadaXSegons(45f)); // Comença a generar flors lentament (cada 45s)
        }
        else if (estat == GameManager.ESTAT_NORMAL) 
        {
            StartCoroutine(GenerarFlorsCadaXSegons(30f)); // Generació a ritme mitjà (cada 30s)
        }
        else if (estat == GameManager.ESTAT_UTOPIC) 
        {
            StartCoroutine(GenerarFlorsCadaXSegons(20f)); // Generació ràpida (cada 20s)
        }
    }

    // Coroutine que crea flors amb un interval
    private System.Collections.IEnumerator GenerarFlorsCadaXSegons(float interval) // Coroutine que s'executa al llarg del temps
    {
        while (contadorFlors < maxFlors)            
        {
            CrearFlor(); // Crea una nova flor
            yield return new WaitForSeconds(interval); 
        }
    }

    // Lògica de creació d'una flor
    private void CrearFlor() 
    {
        // Posició aleatòria dins l’àrea de spawn
        Vector3 posicio = spawnArea.position + new Vector3( // Calcula una posició al voltant del 'spawnArea'
            Random.Range(-5f, 5f),                          // Posició aleatoria X
            0,                                              // Manté Y a 0 (plànol del terra)
            Random.Range(-5f, 5f)                           // Posició aleatoria Z
        );

        GameObject novaFlor = Instantiate(flowerPrefab, posicio, Quaternion.identity); // Instancia el prefab a la posició amb rotació neutra
        florsActives.Add(novaFlor); // Afegeix la flor a la llista de flors actives
        contadorFlors++;

        // Escoltar quan la flor canviï d'estat (per detectar quan mori)
        FlowerState estatFlor = novaFlor.GetComponent<FlowerState>(); // Obté el component FlowerState de la nova flor
        estatFlor.CanviFlor += OnFlorCanvi; // S'inscriu a l'esdeveniment per rebre el canvi d'estat d'aquesta flor
    }

    private void OnFlorCanvi(FlowerState flor, int estat) // Es crida quan una flor canvia d'estat
    {
        if (estat == FlowerState.morta) 
        {
            contadorFlors--;
            florsActives.Remove(flor.gameObject); // Treu la referència de la llista d'actives
            flor.CanviFlor -= OnFlorCanvi; // Evita callbacks futurs d'aquesta flor

            if (contadorFlors <= 0) //si el jugador perd totes les flors tenim un GameOver
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER :( HAS MATAT TOTES LES PLANTES!");

        Time.timeScale = 0f; // para el joc
    }
}
