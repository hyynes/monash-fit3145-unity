using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base code: https://docs.unity3d.com/Manual/ios-handle-game-controller-input.html

//TO ADD - ONLY JUMP IF TOUCHING GROUND
//DOUBLE JUMP - NEED REPLENISH THINGY

public class Jump : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float JumpForce = 13.5f;
    private int JumpNumber = 0;

    public float fallingGravityMultiplier = 1.07f;
    public float maxGravityScale = 4.2f;
    private float originalGravityScale;
    private bool bIsGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        originalGravityScale = _rigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && bIsGrounded)
        {
            if (JumpNumber == 0)
            {
                JumpNumber++;
                
                //don't add force across, only up
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, JumpForce);
            }
        }
    }

    void FixedUpdate()
    {
        if (_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.gravityScale *= fallingGravityMultiplier;
            _rigidbody2D.gravityScale = Mathf.Clamp(_rigidbody2D.gravityScale, 0, maxGravityScale);
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
            bIsGrounded = true;
            JumpNumber = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if player leaves the ground
        if (collision.gameObject.CompareTag("Platform"))
        {
            bIsGrounded = false;
        }
    }
}
