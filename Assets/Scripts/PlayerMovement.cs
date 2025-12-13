using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        _rb.velocity = new Vector2(moveX, moveY) * speed;
    }
}
