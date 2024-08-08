using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal_MOV : MonoBehaviour
{
    
    private Rigidbody2D _rigidbody2D;
    public float Speed = 50.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         _rigidbody2D.AddForce(new Vector2((Input.GetAxis("Horizontal") * Speed * Time.deltaTime), 0.0f));
         /*
        if (Input.GetAxis("Horizontal"))
        {
           
        }
        */
    }
}
