using UnityEngine;
using System;

//les físiques del moviment del jugador canviaran de la següent manera:
//DISTÒPIC: els controls s'invertiran --> dreta serà esquerra, amunt serà avall, etc ...
//NORMAL: controls normals
//UTÒPIC: controls normals amb un mini-turbo per anar més ràpid si es prem la tecla d'espai

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    public Animator animator;

    // Moviment base
    [HideInInspector] public Vector2 movement;

    //Comencem amb estat distòpic
    private int estatActual = GameManager.ESTAT_DISTOPIC;

    // Turbo
    public float turboForce = 6f; //la potència del turbo
    public float turboDuration = 0.10f; //la durada del turbo (és curta perquè no volem un boost molt gran)
    private bool isTurbo = false; //sempre comencareme en false
    private float turboTimer = 0f;

    void Start()
    { //llegim els components del player
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Subscriure'ns al GameManager
        GameManager.Instance.Canvi += OnEstatCanviat;
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.Canvi -= OnEstatCanviat;
    }

    //cridem la funció quan el món canvii d'estat
    void OnEstatCanviat(int nouEstat)
    {
        estatActual = nouEstat;
    }

    void Update()
    {
        // Llegim input cru (sense smoothing)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // --- APLICAR FÍSIQUES SEGONS ESTAT ---
        //fem un switch senzill que controli quan el GameManager canvii d'estat
        switch (estatActual)
        {
            case GameManager.ESTAT_DISTOPIC: //controls invertits
                moveX = -moveX;
                moveY = -moveY;
                break;

            case GameManager.ESTAT_NORMAL:
                // controls normals
                break;

            case GameManager.ESTAT_UTOPIC:
                // controls normals + turbo amb espai
                if (Input.GetKeyDown(KeyCode.Space))
                    TryTurbo();
                break;
        }

        movement = new Vector2(moveX, moveY);

        // Animacions
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", movement.magnitude);
    }

    void FixedUpdate() //comprovem que el jugador en aquell frame està fent o no un turbo
    {
        if (isTurbo)
        { //el turbo té una durada limitada
            turboTimer -= Time.fixedDeltaTime; //cada frame li anem restant temps a la duració
            if (turboTimer <= 0f) //quan el timer arriba a zero el turbo s'acaba
                isTurbo = false;

            return; // mentre dura el turbo, ignorem el moviment normal
        }

        rb.linearVelocity = movement.normalized * speed; //un cop acabat el turbo, tornem al moviment normal
    }

    // --- TURBO ---
    void TryTurbo() 
    {
        if (movement == Vector2.zero)
            return; //si el jugador no es mou en una direcció, no funciona

        StartTurbo(movement.normalized); //posem normalized perque funcioni més suau
    }

    void StartTurbo(Vector2 direction)
    {
        isTurbo = true; //activem el bool a true (fins ara estava en false)
        turboTimer = turboDuration;

        rb.linearVelocity = direction * turboForce; //la velocitat es multiplicarà amb l'impuls de 6F que hem definit a la variable turboForce
    }
}
