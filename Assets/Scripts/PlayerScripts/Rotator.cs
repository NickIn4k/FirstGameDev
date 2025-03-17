using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotator : MonoBehaviour
{
    public Vector2 turn;
    CapsuleCollider plrCc;
    
    // Change the angle restrictions
    public int maxAngleY = 60; // Suggested less than 90
    public int minAngleY = -30; // Suggested more than -90
    public float sensitivity = 0.5f; // Sensitivity
    public float heightFromPlayer = 0.1f; // Height from player center of gravity

    private bool shouldInvert = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plrCc = GeneralMethods.GetPlayer().GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = plrCc.bounds.center + new Vector3(0, heightFromPlayer);

        // Extract delta
        var delta = Mouse.current.delta.ReadValue() * (sensitivity * Time.deltaTime);
        
        // Update rotation values
        turn.x += delta.x;
        
        // Consider inverted
        if (shouldInvert)
            turn.y -= delta.y ;
        else 
            turn.y += delta.y;
        
        
        // Apply camera restrictions
        if (turn.y > maxAngleY)
            turn.y = maxAngleY;
        else if (turn.y < minAngleY)
            turn.y = minAngleY;
        
        transform.localRotation = Quaternion.Euler(turn.y, turn.x, 0);
    }

    public void SetInvert(bool value)
    {
        shouldInvert = value;
    }
}