using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base code: https://docs.unity3d.com/Manual/ios-handle-game-controller-input.html

//TO ADD - ONLY JUMP IF TOUCHING GROUND
//DOUBLE JUMP - NEED REPLENISH THINGY

public class Jump : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float JumpForce = 50.0f;
    private int JumpNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (JumpNumber == 0)
            {
                JumpNumber++;
                //don't add force across, only up
                _rigidbody2D.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        Debug.Log("Collision");
        if (collision2D.gameObject.CompareTag("Platform"))
        {
            Debug.Log("Floor Collide");
            JumpNumber = 0;
        }
    }
}
