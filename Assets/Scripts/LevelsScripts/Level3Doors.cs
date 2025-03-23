using System;
using UnityEngine;

public class Level3Doors : MonoBehaviour
{
    public GameObject[] platesFirst;
    public GameObject[] platesSecond;
    public GameObject[] leversFirst;

    public GameObject doorFirstGameObject;
    private DoorOpener doorFirst;
    
    public GameObject doorHexagonGameObject;
    private DoorOpener doorHexagon;
    
    public GameObject doorSecondGameObject;
    private DoorOpener doorSecond;

    public GameObject hexagonMinigame;
    private HexagonChecker hexagonChecker;

    private int required;
    private int pressed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        required = platesFirst.Length;

        foreach (var plate in platesFirst)
        {
            plate.GetComponent<DetectPressed>().OnPressed += PlateOnPressed;
            plate.GetComponent<DetectPressed>().OnReleased += PlateOnReleased;
        }
        
        foreach (var plate in platesSecond)
        {
            plate.GetComponent<DetectPressed>().OnPressed += PlateOnPressed;
            plate.GetComponent<DetectPressed>().OnReleased += PlateOnReleased;
        }
        
        doorFirst = doorFirstGameObject.GetComponent<DoorOpener>();
        doorHexagon = doorHexagonGameObject.GetComponent<DoorOpener>();
        doorSecond = doorSecondGameObject.GetComponent<DoorOpener>();
        
        hexagonChecker = hexagonMinigame.GetComponent<HexagonChecker>();
        hexagonChecker.OnCompletion += OpenHexagonDoor;
    }

    private void OnDisable()
    {
        DisablePlates(platesFirst);
        DisablePlates(platesSecond);
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
        if (pressed >= required && required == platesFirst.Length)
        {
            pressed = 0;
            doorFirst.OpenDoor();
            DisablePlates(platesFirst);
            required = platesSecond.Length;
        }
        else if (pressed >= required && required == platesSecond.Length)
        {
            pressed = 0;
            doorSecond.OpenDoor();
            DisablePlates(platesSecond);
            required = 999;
        }
    }
    
    private void OpenHexagonDoor()
    {
        doorHexagon.OpenDoor();
    }
}
