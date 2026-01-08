using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    public Animator animator;

    // Moviment base
    [HideInInspector] public Vector2 movement;

    // Estat actual del món
    private int estatActual = GameManager.ESTAT_NORMAL;

    // Turbo
    public float turboForce = 6f;
    public float turboDuration = 0.10f;
    private bool isTurbo = false;
    private float turboTimer = 0f;

    void Start()
    {
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
        switch (estatActual)
        {
            case GameManager.ESTAT_DISTOPIC:
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

    void FixedUpdate()
    {
        if (isTurbo)
        {
            turboTimer -= Time.fixedDeltaTime;
            if (turboTimer <= 0f)
                isTurbo = false;

            return; // mentre dura el turbo, ignorem el moviment normal
        }

        rb.linearVelocity = movement.normalized * speed;
    }

    // --- TURBO ---
    void TryTurbo()
    {
        if (movement == Vector2.zero)
            return; // no turbo si no hi ha direcció

        StartTurbo(movement.normalized);
    }

    void StartTurbo(Vector2 direction)
    {
        isTurbo = true;
        turboTimer = turboDuration;

        rb.linearVelocity = direction * turboForce;
    }
}
