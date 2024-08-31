using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script was modified from a similar camera movement script in FIT3039 (coded by Danny)
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float MinimumCameraX;
    [SerializeField] private float MinimumCameraY;
    [SerializeField] private float MaximumCameraX;
    [SerializeField] private float MaximumCameraY;

    [SerializeField] private float CameraSpeed = 5f;

    private Transform Player;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            // Move the camera to follow the player
            Vector3 NewCameraPosition = new Vector3(Player.position.x, Player.position.y, transform.position.z);

            // Clamp the camera position within the bounds
            NewCameraPosition.x = Mathf.Clamp(NewCameraPosition.x, MinimumCameraX, MaximumCameraX);
            NewCameraPosition.y = Mathf.Clamp(NewCameraPosition.y, MinimumCameraY, MaximumCameraY);

            // Smoothly move the camera towards the new position
            transform.position = Vector3.Lerp(transform.position, NewCameraPosition, CameraSpeed * Time.deltaTime);

        }
    }
}