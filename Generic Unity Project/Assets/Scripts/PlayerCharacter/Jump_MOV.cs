using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//base code: https://docs.unity3d.com/Manual/ios-handle-game-controller-input.html

//TO ADD - ONLY JUMP IF TOUCHING GROUND
//DOUBLE JUMP - NEED REPLENISH THINGY

public class Jump : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    public float JumpForce = 13.5f;
    public float maxJumps = 2;
    private int JumpNumber = 0;

    public float fallingGravityMultiplier = 1.07f;
    public float maxGravityScale = 4.2f;
    private float originalGravityScale;

    public bool bIsGliding = false;
    public float glideGravityScale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        originalGravityScale = _rigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (JumpNumber >= maxJumps)
        {
            if (Input.GetButton("Jump"))
            {
                bIsGliding = true;
            }
            else
            {
                bIsGliding = false;
            }
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            if (JumpNumber < maxJumps)
            {
                JumpNumber++;
                bIsGliding = false;
                
                //don't add force across, only up
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, JumpForce);
            }
        }
    }

    void FixedUpdate()
    {
        if (_rigidbody2D.velocity.y <= 0)
        {
            if (bIsGliding)
            {
                _rigidbody2D.gravityScale = glideGravityScale;
            }
            else
            {
                _rigidbody2D.gravityScale *= fallingGravityMultiplier;
                _rigidbody2D.gravityScale = Mathf.Clamp(_rigidbody2D.gravityScale, 0, maxGravityScale);   
            }
        }
        else
        {
            _rigidbody2D.gravityScale = originalGravityScale;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player is on the ground
        if (collision.gameObject.CompareTag("Platform"))
        {
            JumpNumber = 0;
            bIsGliding = false;
        }
    }
}
