using UnityEngine;

public class PlayerVelocity : MonoBehaviour
{
    PlayerMovement PlayerMovement;

    //SON VALORS QUE HEM DE PROVAR, S'HAN D'AJUSTAR
    public float velocitat_DISTOPICA = 1.5f;
    public float velocitat_NORMAL = 2.3f;
    public float velocitat_UTOPICA = 4f;


    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>(); //llegim el moviment del jugador
    }

    void Update()
    {
        switch (GameManager.estatActual)
        { //segons l'estat actual, la velocitat canviarà:
            case GameManager.ESTAT_DISTOPIC:

                PlayerMovement.SetSpeed(velocitat_DISTOPICA);
                break;

            case GameManager.ESTAT_NEUTRE:

                PlayerMovement.SetSpeed(velocitat_NORMAL);
                break;

            case GameManager.ESTAT_UTOPIC:

                PlayerMovement.SetSpeed(velocitat_UTOPICA);
                break;
        }
    }
}
