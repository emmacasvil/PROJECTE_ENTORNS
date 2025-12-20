using UnityEngine;
using System;

public class PlayerVelocity : MonoBehaviour
{
    PlayerMovement playerMovement;

    // SON VALORS QUE HEM DE PROVAR, S'HAN D'AJUSTAR
    public float velocitat_DISTOPICA = 1.5f;
    public float velocitat_NORMAL = 2.3f;
    public float velocitat_UTOPICA = 4f;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); // llegim el moviment del jugador
    }

    void Update() //fwm un switch normal
    { //ES FARÀ UNA IMPLEMENTACIÓ EN UN FUTUR PERQUÈ SIGUI GRADUAL I NO UN CANVI SOBTAT
        switch (GameManager.Instance.estatActual)
        {
            case GameManager.ESTAT_DISTOPIC:
                playerMovement.speed = velocitat_DISTOPICA;
            break;


            case GameManager.ESTAT_NORMAL:
                playerMovement.speed = velocitat_NORMAL;
            break;


            case GameManager.ESTAT_UTOPIC:
                playerMovement.speed = velocitat_UTOPICA;
            break;
        }
    }
}