using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector2 turn;
    public Transform plr;

    // Change the angle restrictions
    public int maxAngleY = 60; // Suggested less than 90
    public int minAngleY = -40; // Suggested more than -90
    public float Sensitivity = .5f; // Sensitivity
    public float heightFromPlayer = .5f; // Height from player center of gravity

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = plr.position + new Vector3(0, heightFromPlayer);

        // Handle mouse movement
        turn.x += Input.GetAxis("Mouse X") * Sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * Sensitivity;

        // Camera restrictions
        if (turn.y > maxAngleY)
            turn.y = maxAngleY;
        else if (turn.y < minAngleY)
            turn.y = minAngleY;

        // Camera rotation
        transform.localRotation = Quaternion.Euler(turn.y, turn.x, 0);
    }
}
