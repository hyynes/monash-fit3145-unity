using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinBehaviour : MonoBehaviour
{
    public TextMeshProUGUI TextComponent;
    private Jump JumpScript;
    private Rigidbody2D _rigidbody2D;
    private Horizontal_MOV MovementScript;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        TextComponent.text = string.Empty;
        JumpScript = GetComponent<Jump>();
        MovementScript = GetComponent<Horizontal_MOV>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.CompareTag("Win"))
        {
            WinTriggerBehaviour WinTrigger = Collision.GetComponent<WinTriggerBehaviour>();
            WinTrigger.UpdateSprite();
            Win();
        }
    }
    
    void Win()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.gravityScale = 0;
        JumpScript.enabled = false;
        MovementScript.enabled = false;
        TextComponent.text = "You win!";
    }
}
