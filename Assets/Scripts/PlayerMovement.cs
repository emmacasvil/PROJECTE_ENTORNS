using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4f;
    private Rigidbody2D _rb;
    float Move_X, Move_Y;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
       Move_X = Input.GetAxis("Horizontal") * speed;
       Move_Y = Input.GetAxis("Vertical") * speed;
    }

    void Update()
    {
        _rb.velocity = new Vector2(Move_X, Move_Y);
    }


}