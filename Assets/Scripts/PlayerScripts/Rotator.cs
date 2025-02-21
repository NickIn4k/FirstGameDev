using Unity.VisualScripting;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Vector2 turn;
    Transform plr;

    // Change the angle restrictions
    public int maxAngleY = 60; // Suggested less than 90
    public int minAngleY = -40; // Suggested more than -90
    public float Sensitivity = 0.5f; // Sensitivity
    public float heightFromPlayer = .5f; // Height from player center of gravity

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plr = GeneralMethods.GetPlayer().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = plr.position + new Vector3(0, heightFromPlayer);
        transform.position = newPos;

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
