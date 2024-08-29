using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float minimumCameraX;
    [SerializeField] private float minimumCameraY;
    [SerializeField] private float maximumCameraX;
    [SerializeField] private float maximumCameraY;

    [SerializeField] private float cameraSpeed = 5f;

    private Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            // Move the camera to follow the player
            Vector3 newCameraPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            // Clamp the camera position within the bounds
            newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, minimumCameraX, maximumCameraX);
            newCameraPosition.y = Mathf.Clamp(newCameraPosition.y, minimumCameraY, maximumCameraY);

            // Smoothly move the camera towards the new position
            transform.position = Vector3.Lerp(transform.position, newCameraPosition, cameraSpeed * Time.deltaTime);

        }
    }
}