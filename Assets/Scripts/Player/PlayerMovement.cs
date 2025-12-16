using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody2D _rb; //el player t� un rigidbody 2D enlla�at

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); //llegim el rigidbody
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); //segons l'input del jugador es mour� en horitzontal
        float moveY = Input.GetAxis("Vertical"); //el mateix per� eb vertical

        _rb.linearVelocity = new Vector2(moveX, moveY) * speed;
    }
}
