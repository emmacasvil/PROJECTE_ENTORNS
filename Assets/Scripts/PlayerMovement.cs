using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerMovement _movement;
    public float speed = 4f;
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
       float Move_X = Input.GetAxis("Horizontal");
       float Move_Y = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        _rb.Velocity = new Vector3(Move_X, Move_Y) * speed;
    }


}



