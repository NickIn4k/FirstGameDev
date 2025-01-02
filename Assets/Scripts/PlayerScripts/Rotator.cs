using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector2 turn;
    public Transform plr;

    // Change the angle restrictions
    public int maxAngleY = 70; // Suggested less than 90
    public int minAngleY = -40; // Suggested more than -90
    public float Sensitivity = .5f; // Sensitivity

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = plr.position;

        // Handle mouse movement
        turn.x += Input.GetAxis("Mouse X") * Sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * Sensitivity;

        // Camera restrictions
        if (turn.y > maxAngleY)
            turn.y = maxAngleY;
        else if (turn.y < minAngleY)
            turn.y = minAngleY;

        // Camera rotation
        transform.localRotation = Quaternion.Euler(0, turn.x, -turn.y);
    }
}
