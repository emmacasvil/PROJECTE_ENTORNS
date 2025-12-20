using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody2D _rb; //la variable de rigidbody

    public Animator animator; //llegirem el animator perquè es coordini amb les animacions

    Vector2 movement;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); //llegim el rigidbody i el animator de l'escena de unity
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); //fletxes esquerra/dreta
        float moveY = Input.GetAxis("Vertical"); //fletxes amunt/avall
        //aquestes variables retornen valors entre 1 i -1, que ens servirà per ajustar valors amb les animacions (visualitzar animator)

        movement = new Vector2(moveX, moveY); //generem un nou vector que és la direcció cap a on es vol moure el jugador

        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", movement.magnitude);

        _rb.linearVelocity = movement * speed; //Mou el jugador multiplicant la direcció per la velocitat. linearVelocity fa que el moviment sigui suau
    }
}
