using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// base code: https://docs.unity3d.com/Manual/ios-handle-game-controller-input.html

public class Jump : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    public float JumpForce = 13.5f;
    public float MaxJumps = 2;
    public int JumpNumber = 0;

    public float FallingGravityMultiplier = 1.07f;
    public float MaxGravityScale = 4.2f;
    public float OriginalGravityScale;

    public bool bIsGliding = false;
    public float GlideGravityScale = 1f;
    private SpriteRenderer SpriteRenderer;
    private Sprite SpriteBeforeGliding;
    [SerializeField] private Sprite JumpingSprite;
    [SerializeField] private Sprite GlidingSprite;

    // Start is called before the first frame update
    void Start()
    {
        // get necessary components
        _rigidbody2D = GetComponent<Rigidbody2D>();
        OriginalGravityScale = _rigidbody2D.gravityScale;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteBeforeGliding = SpriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        // gliding
        if (JumpNumber >= MaxJumps)
        {
            if (Input.GetButton("Jump"))
            {
                bIsGliding = true;
            }
            else
            {
                if (JumpingSprite)
                {
                    SpriteRenderer.sprite = JumpingSprite;
                    bIsGliding = false;
                }
            }
        }
        
        // jump mechanism
        if (Input.GetButtonDown("Jump"))
        {
            if (JumpNumber < MaxJumps)
            {
                JumpNumber++;
                
                if (JumpingSprite)
                {
                    SpriteRenderer.sprite = JumpingSprite;   
                }
                
                //don't add force across, only up
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, JumpForce);
            }
        }
    }

    
    // fixed update changes gravity scale according to player action
    void FixedUpdate()
    {
        // ensure that gravity scale increases when player falls down; removes "floaty" feeling
        if (_rigidbody2D.velocity.y <= 0)
        {
            
            // if the player is currently gliding, update their sprite and change the gravity scale accordingly
            if (bIsGliding)
            {
                _rigidbody2D.gravityScale = GlideGravityScale;
                if (GlidingSprite)
                {
                    SpriteRenderer.sprite = GlidingSprite;   
                }
            }
            
            // otherwise, increase the gravity scale by a multiplier
            else
            {
                _rigidbody2D.gravityScale *= FallingGravityMultiplier;
                _rigidbody2D.gravityScale = Mathf.Clamp(_rigidbody2D.gravityScale, 0, MaxGravityScale);   
            }
            
            // if the velocity is above zero, don't change the gravity scale
        }
        else
        {
            _rigidbody2D.gravityScale = OriginalGravityScale;
        }
    }
    
    // responsible for checking jump crystal behaviour
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // jump crystal behaviour
        if (collision.CompareTag("Sugar"))
        {
            JumpCrystalBehaviour crystalBehaviour = collision.gameObject.GetComponent<JumpCrystalBehaviour>();
            
            // if the jump crystal is available, reset the number of jumps
            if (crystalBehaviour.bIsAvailable)
            {
                JumpNumber = 0;
                crystalBehaviour.DestroyThenRefresh();   
            }
        }
    }

    // responsible for refreshing jump number on platform landing
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player is on the ground, if they are, reset the jump number and if they were gliding, reset their sprite
        if (collision.gameObject.CompareTag("Platform"))
        {
            JumpNumber = 0;
            StopGliding();
        }
    }

    // responsible for resetting player sprite and gliding state
    private void StopGliding()
    {
        bIsGliding = false;
        SpriteRenderer.sprite = SpriteBeforeGliding;
    }
}
