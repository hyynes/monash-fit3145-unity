using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    private Transform RespawnPoint;
    public Transform StartingSpawn;
        
    public float RespawnDelay = 1;
    
    // player states
    private Rigidbody2D _rigidbody2D;
    private Jump JumpScript;
    private Horizontal_MOV MovementScript;
    
    // Start is called before the first frame update
    void Start()
    {
        // get necessary components
        _rigidbody2D = GetComponent<Rigidbody2D>();
        JumpScript = GetComponent<Jump>();
        MovementScript = GetComponent<Horizontal_MOV>();
        
        // starting spawn needs to be initialised in engine; otherwise the player respawns at their position of death
        if (StartingSpawn)
        {
            RespawnPoint = StartingSpawn;  
        }
        else
        {
            RespawnPoint = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    // when the player touches a hazard, they die
    void Die()
    {
        // Disable player controls and set movement vector to zero
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        JumpScript.enabled = false;
        MovementScript.enabled = false;

        // Start the respawn process
        StartCoroutine(Respawn());
    }

    // after the respawn timer delay, the player respawns at their respawn location
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(RespawnDelay);

        // Reset player's position to the respawn point
        transform.position = RespawnPoint.position;

        // Reset states
        _rigidbody2D.gravityScale = JumpScript.OriginalGravityScale;
        GetComponent<Collider2D>().enabled = true;
        JumpScript.enabled = true;
        MovementScript.enabled = true;
        JumpScript.JumpNumber = 0; // Reset jump count
    }
    
    // check if the player is on a hazard; if they are, call the die method
    // otherwise, if they are on a checkpoint, reset their respawn point
    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.CompareTag("Hazard"))
        {
            Debug.Log("die");
            Die();
        }
        else if (Collision.CompareTag("Checkpoint"))
        {
            setRespawnPoint(Collision.transform);
        }
    }
    
    // reset player spawn point
    public void setRespawnPoint(Transform RespawnPoint)
    {
        this.RespawnPoint = RespawnPoint;
    }
}
