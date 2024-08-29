using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//base code: https://docs.unity3d.com/Manual/ios-handle-game-controller-input.html

public class Jump : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    public float JumpForce = 13.5f;
    public float maxJumps = 2;
    public int JumpNumber = 0;

    public float fallingGravityMultiplier = 1.07f;
    public float maxGravityScale = 4.2f;
    public float originalGravityScale;

    public bool bIsGliding = false;
    public float glideGravityScale = 1f;
    private SpriteRenderer spriteRenderer;
    private Sprite spriteBeforeGliding;
    [SerializeField] private Sprite glidingSprite;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        originalGravityScale = _rigidbody2D.gravityScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteBeforeGliding = spriteRenderer.sprite;
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
                StopGliding();
            }
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            if (JumpNumber < maxJumps)
            {
                JumpNumber++;
                StopGliding();
                
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
                if (glidingSprite)
                {
                    spriteRenderer.sprite = glidingSprite;   
                }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sugar"))
        {
            JumpCrystalBehaviour crystalBehaviour = collision.gameObject.GetComponent<JumpCrystalBehaviour>();
            
            if (crystalBehaviour.bIsAvailable)
            {
                JumpNumber = 0;
                crystalBehaviour.DestroyThenRefresh();   
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player is on the ground
        if (collision.gameObject.CompareTag("Platform"))
        {
            JumpNumber = 0;
            StopGliding();
        }
    }

    private void StopGliding()
    {
        bIsGliding = false;
        spriteRenderer.sprite = spriteBeforeGliding;
    }
}
