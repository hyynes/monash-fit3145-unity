using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal_MOV : MonoBehaviour
{
    
    private Rigidbody2D _rigidbody2D;
    public float acceleration = 50.0f;
    public float maximumVelocity = 30.0f;
    public float horizontalVelocity = 0.0f;
        
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        float horizontalAcceleration = Input.GetAxis("Horizontal") * acceleration;
        Vector2 forceToAdd = new Vector2(horizontalAcceleration, 0.0f);

        horizontalVelocity = _rigidbody2D.velocity.x;
        _rigidbody2D.AddForce(forceToAdd);

        float horizontalMovement = Mathf.Clamp(_rigidbody2D.velocity.x, -maximumVelocity, maximumVelocity);
        _rigidbody2D.velocity = new Vector2(horizontalMovement, _rigidbody2D.velocity.y);
    }
}
