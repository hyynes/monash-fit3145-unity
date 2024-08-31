using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// NOT USED IN CURRENT IMPLEMENTATION
public class Dash_MOV : MonoBehaviour
{
    //facing direction
    private Rigidbody2D _rigidbody2D;
    private bool CanDash;
    private bool TouchingFloor;
    public float DashSpeed = 100.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Dash"))
        {
            if (CanDash || TouchingFloor)
            {
                CanDash = false;
                _rigidbody2D.AddForce(new Vector2(Input.GetAxis("Horizontal") * DashSpeed, 0f), ForceMode2D.Impulse);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        Debug.Log("Collision");
        if (collision2D.gameObject.CompareTag("Platform"))
        {
            Debug.Log("Floor Collide");
            TouchingFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision2D)
    {
        Debug.Log("Collision");
        if (collision2D.gameObject.CompareTag("Platform"))
        {
            Debug.Log("Floor Collide");
            TouchingFloor = false;
            CanDash = true;
        }
    }
}
