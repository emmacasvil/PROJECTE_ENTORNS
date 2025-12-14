using UnityEngine;                         // Importa l'API de Unity (GameObject, MonoBehaviour, etc.)
using System.Collections.Generic;          // Permet usar llista genèrica List<T>

public class FlowerGenerator : MonoBehaviour // Defineix una classe que hereta de MonoBehaviour (component de Unity)
{
    [SerializeField] private GameObject flowerPrefab; // Prefab de la flor assignable des de l'Inspector
    [SerializeField] private Transform spawnArea;     // Transform que defineix el centre de la zona d'aparició
    [SerializeField] private int maxFlors = 15;       // Nombre màxim de flors que poden existir alhora

    private List<GameObject> florsActives = new List<GameObject>(); // Llista per guardar referències a les flors creades
    private int contadorFlors = 0;                                      // Comptador de flors vives/actives

    private void OnEnable()                         // Es crida quan el component s'activa (o l'objecte s'activa)
    {
        GameManager.Instance.Canvi += Reaccio;     // S'inscriu a l'esdeveniment "Canvi" del GameManager per rebre notificacions d'estat
    }

    private void OnDisable()                        // Es crida quan el component es desactiva
    {
        GameManager.Instance.Canvi -= Reaccio;     // Es desinscriu de l'esdeveniment per evitar crides quan no toca i fugues de memòria
    }

    // Aquesta funció s’executa quan el GameManager canvia d’estat
    void Reaccio(int estat)                         // Callback que rep el nou estat del joc (distòpic/normal/utòpic)
    {
        StopAllCoroutines();                        // Atura qualsevol coroutine de generació en marxa per no solapar ritmes de spawn

        if (estat == GameManager.ESTAT_DISTOPIC)    // Si el joc és distòpic...
        {
            StartCoroutine(GenerarFlorsCadaXSegons(8f)); // Comença a generar flors lentament (cada 8s)
        }
        else if (estat == GameManager.ESTAT_NORMAL) // Si l'estat és normal...
        {
            StartCoroutine(GenerarFlorsCadaXSegons(4f)); // Generació a ritme mitjà (cada 4s)
        }
        else if (estat == GameManager.ESTAT_UTOPIC) // Si és utòpic...
        {
            StartCoroutine(GenerarFlorsCadaXSegons(2f)); // Generació ràpida (cada 2s)
        }
    }

    // Coroutine que crea flors amb un interval
    private System.Collections.IEnumerator GenerarFlorsCadaXSegons(float interval) // Coroutine que s'executa al llarg del temps
    {
        while (contadorFlors < maxFlors)            // Mentre no s'arribi al màxim de flors actives...
        {
            CrearFlor();                            // Crea una nova flor
            yield return new WaitForSeconds(interval); // Espera l'interval abans de crear-ne una altra
        }

        // Quan arribem al màxim, avisem al GameManager que canvii d’estat
        Debug.Log("S'ha arribat al màxim de flors!");       // Missatge informatiu a la consola
        GameManager.Instance.CanviarEstat(GameManager.ESTAT_DISTOPIC);
        // Exemple: al màxim, tornem l'estat a distòpic (pots canviar la lògica segons el teu disseny)
    }

    private void CrearFlor()                        // Encapsula la lògica de creació d'una flor
    {
        // Posició aleatòria dins l’àrea de spawn
        Vector3 posicio = spawnArea.position + new Vector3( // Calcula una posició al voltant del 'spawnArea'
            Random.Range(-5f, 5f),                          // Desplaçament aleatori en X
            0,                                              // Manté Y a 0 (plànol del terra)
            Random.Range(-5f, 5f)                           // Desplaçament aleatori en Z
        );

        GameObject novaFlor = Instantiate(flowerPrefab, posicio, Quaternion.identity); // Instancia el prefab a la posició amb rotació neutra
        florsActives.Add(novaFlor);                       // Afegeix la flor a la llista de flors actives
        contadorFlors++;                                  // Incrementa el comptador de flors

        // Escoltar quan la flor canviï d'estat (per detectar quan mori)
        FlowerState estatFlor = novaFlor.GetComponent<FlowerState>(); // Obté el component FlowerState de la nova flor
        estatFlor.CanviFlor += OnFlorCanvi;               // S'inscriu a l'esdeveniment per rebre el canvi d'estat d'aquesta flor
    }

    private void OnFlorCanvi(FlowerState flor, int estat) // Callback cridat quan una flor canvia d'estat
    {
        if (estat == FlowerState.morta)                   // Si l'estat és 'morta'...
        {
            contadorFlors--;                              // Decrementa el comptador de flors actives
            florsActives.Remove(flor.gameObject);         // Treu la referència de la llista d'actives
            // Opcional: desinscriure l'event per seguretat si la flor s'autodestrueix
            flor.CanviFlor -= OnFlorCanvi;                // Evita callbacks futurs d'aquesta flor
        }
    }
}
