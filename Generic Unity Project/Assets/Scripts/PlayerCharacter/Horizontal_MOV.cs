using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// This script was modified from a similar horizontal movement script in FIT3039 (coded by Danny)
public class Horizontal_MOV : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer SpriteRenderer;
    
    public float Speed = 5;
        
    // Start is called before the first frame update
    void Start()
    {
        // get necessary components
        _rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // responsible for setting character's translation in the x axis
    void Update()
    {
        // get horizontal direction
        float HorizontalDirection = Input.GetAxis("Horizontal");
        
        // translate the player along that direction with the speed
        _rigidbody2D.velocity = new Vector2(HorizontalDirection * Speed, _rigidbody2D.velocity.y);
        
        // flip sprite according to horizontal direction
        if (HorizontalDirection > 0)
        {
            SpriteRenderer.flipX = false;
        }
        else if (HorizontalDirection < 0)
        {
            SpriteRenderer.flipX = true;
        }
        
    }
}
