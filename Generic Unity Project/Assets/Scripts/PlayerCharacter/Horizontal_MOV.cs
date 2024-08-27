using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal_MOV : MonoBehaviour
{
    
    private Rigidbody2D _rigidbody2D;
    public float speed = 5;
        
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float horizontalDirection = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(horizontalDirection * speed, _rigidbody2D.velocity.y);
    }
}
