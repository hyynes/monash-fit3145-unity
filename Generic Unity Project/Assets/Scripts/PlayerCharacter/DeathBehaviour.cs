using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    private Transform respawnPoint;
    public Transform startingSpawn;
        
    public float respawnDelay = 3;
    
    // player states
    private Rigidbody2D _rigidbody2D;
    private Jump jumpScript;
    private Horizontal_MOV movementScript;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        jumpScript = GetComponent<Jump>();
        movementScript = GetComponent<Horizontal_MOV>();
        
        if (startingSpawn)
        {
            respawnPoint = startingSpawn;  
        }
        else
        {
            respawnPoint = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    void Die()
    {
        // Disable player controls
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        jumpScript.enabled = false;
        movementScript.enabled = false;

        // Start the respawn process
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);

        // Reset player's position to the respawn point
        transform.position = respawnPoint.position;

        // Reset states
        _rigidbody2D.gravityScale = jumpScript.originalGravityScale;
        GetComponent<Collider2D>().enabled = true;
        jumpScript.enabled = true;
        movementScript.enabled = true;
        jumpScript.JumpNumber = 0; // Reset jump count
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            Debug.Log("die");
            Die();
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            setRespawnPoint(collision.transform);
        }
    }
    
    public void setRespawnPoint(Transform respawnPoint)
    {
        this.respawnPoint = respawnPoint;
    }
}
