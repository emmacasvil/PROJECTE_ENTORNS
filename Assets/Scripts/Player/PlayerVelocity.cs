using UnityEngine;
using System;
using System.Collections;

public class PlayerVelocity : MonoBehaviour
{
    PlayerMovement playerMovement;

    public float velocitatBase = 1.5f;   // comença lent
    public float increment = 0.15f;      // +0.10f per cada valorEstat

    float velocitatTARGET;
    public float velocitatCanvi = 2f; //aquesta velocitat no afecta directament al jugador, té a veure amb com de ràpid el Lerp canvia de la velocitat antiga a la nova

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        playerMovement.speed = velocitatBase; //per evitar problemes de sobreescriure, li diem que la velocitat que es fa servir en el script del player movement sigui la velocitat inicial d'aquest script, osigui 1,5f
        velocitatTARGET = velocitatBase;
    }

    void OnEnable()
    {
        StartCoroutine(EsperarGameManager()); //hem hagut de fer una corutina perquè aquest script s'activava abans del GameManager i llavors NO es printejaven els debug
    }

    IEnumerator EsperarGameManager() //quan s'hagi carregat el GameManager comprovarà:
    {
        // Espera fins que GameManager.Instance no sigui null
        while (GameManager.Instance == null)
        {
            yield return null; // espera un frame
        }

        Debug.Log("[PL.VELOCITY] Subscrit correctament a ValorModificat");
        GameManager.Instance.ValorModificat += ReaccioGradual;
    }


    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ValorModificat -= ReaccioGradual;
    }

    void Update() //per fer els canvis graduals, tan si com no hem de fer un Lerp
    {
        playerMovement.speed = Mathf.Lerp(playerMovement.speed, velocitatTARGET, Time.deltaTime * velocitatCanvi);
        //el Lerp NO funciona depenent del valorEstat FUNCIONA PER FRAMES, cada frame, la speed anirà apropant-se a la velocitatTARGET (calculada a la següent funció), i ho farà amb la velocitat establerta per la varible: velocitatCanvi, un número baix (com és el nostre cas) farà el canvi suaument, un número més alt (10 per exemple) farà el canvi instantani
    }

    void ReaccioGradual(float valorEstat) //aquesta funció es dispara quan el GameManager crida que hi ha hagut un canvi en el valorEstat (osigui el jugador ha fet una acicó que ha fet pujar o baixat el valor del món)

    {
        velocitatTARGET = velocitatBase + valorEstat * increment; //aquesta formula és la que es serveix per calcular quin valor li correspon
        //per entendre-ho faig un exemple: valorEstat = 1, velocitatBase = 1.5f, velocitatTARGET = 1.5 + (1*0,15) -> 1.5 + 0,15 --> VelcotatTARGET = 1,65
          
        Debug.Log($"[VELOCITAT] valorEstat={valorEstat} → velocitatTARGET={velocitatTARGET}"); //fem un debug per comprovar que els canvis s'apliquin correctament
    }
}
