using UnityEngine;

public class PlayerVelocity : MonoBehaviour
{
    PlayerMovement playerMovement;
    GameManager gameManager;

    // SON VALORS QUE HEM DE PROVAR, S'HAN D'AJUSTAR
    public float velocitat_DISTOPICA = 1.5f;
    public float velocitat_NORMAL = 2.3f;
    public float velocitat_UTOPICA = 4f;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); // llegim el moviment del jugador
    }

    void Update()
    {
        switch (GameManager.estatActual)
        {
            case gameManager.ESTAT_DISTOPIC:
                playerMovement.speed(velocitat_DISTOPICA);
                break;

            case gameManager.ESTAT_NORMAL:
                playerMovement.speed(velocitat_NORMAL);
                break;

            case gameManager.ESTAT_UTOPIC:
                playerMovement.speed(velocitat_UTOPICA);
                break;
        }
    }
}