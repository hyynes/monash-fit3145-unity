using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal_MOV : MonoBehaviour
{
    
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer spriteRenderer;
    
    public float speed = 5;
        
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        float horizontalDirection = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(horizontalDirection * speed, _rigidbody2D.velocity.y);
        
        if (horizontalDirection > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalDirection < 0)
        {
            spriteRenderer.flipX = true;
        }
        
    }
}
