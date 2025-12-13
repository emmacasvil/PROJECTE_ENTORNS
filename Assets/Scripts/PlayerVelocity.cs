using UnityEngine;

public class PlayerVelocity : MonoBehaviour
{
    PlayerMovement _plaMovement;

    //SON VALORS QUE HEM DE PROVAR, S'HAN D'AJUSTAR
    public float velocitat_DISTOPICA = 1.5f;
    public float velocitat_NEUTRE = 2.3f;
    public float velocitat_UTOPICA = 4f;


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); //llegim el moviment del jugador
    }

    void Update()
    {
        switch (GameManager.estatActual)
        { //segons l'estat actual, la velocitat canviarà:
            case GameManager.ESTAT_DISTOPIC:
               
                playerMovement.SetSpeed(velocitat_DISTOPICA);
                break;

            case GameManager.ESTAT_NEUTRE:
                
                playerMovement.SetSpeed(velocitat_NEUTRE);
                break;

            case GameManager.ESTAT_UTOPIC:
                
                playerMovement.SetSpeed(velocitat_UTOPICA);
                break;
        }
    }
}
