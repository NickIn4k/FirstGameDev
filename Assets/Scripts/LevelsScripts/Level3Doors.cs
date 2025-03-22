using System;
using UnityEngine;

public class Level3Doors : MonoBehaviour
{
    public GameObject[] platesFirst;
    public GameObject[] platesSecond;
    public GameObject[] leversFirst;

    public GameObject doorFirstGameObject;
    private DoorOpener doorFirst;

    private int requiredFirst;
    private int pressed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        requiredFirst = platesFirst.Length;

        foreach (var plate in platesFirst)
        {
            plate.GetComponent<DetectPressed>().OnPressed += PlateOnPressed;
            plate.GetComponent<DetectPressed>().OnReleased += PlateOnReleased;
        }
        
        doorFirst = doorFirstGameObject.GetComponent<DoorOpener>();
    }
    
    private void OnDisable()
    {
        DisablePlates(platesFirst);
    }

    private void DisablePlates(GameObject[] plates)
    {
        foreach (var plate in plates)
        {
            plate.GetComponent<DetectPressed>().OnPressed -= PlateOnPressed;
            plate.GetComponent<DetectPressed>().OnReleased -= PlateOnReleased;
        }
    }
    
    private void PlateOnPressed()
    {
        pressed++;
    }
    
    private void PlateOnReleased()
    {
        pressed--;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed >= requiredFirst)
        {
            pressed = 0;
            doorFirst.OpenDoor();
            DisablePlates(platesFirst);
            requiredFirst = 999;
        }
        
    }
}
