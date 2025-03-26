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
    
    public GameObject doorThirdGameObject;
    private DoorOpener doorThird;

    public GameObject hexagonMinigame;
    private HexagonChecker hexagonChecker;

    private int required;
    private int pressed;

    private bool secondOpened = false;
    
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

        foreach (var lever in leversFirst)
        {
            lever.GetComponent<LeverOpener>().OnPressed += PlateOnPressed;
            lever.GetComponent<LeverOpener>().OnReleased += PlateOnReleased;
            lever.GetComponent<LeverInteract>().OnInteract += OnInteract;
        }
        
        doorFirst = doorFirstGameObject.GetComponent<DoorOpener>();
        doorHexagon = doorHexagonGameObject.GetComponent<DoorOpener>();
        doorSecond = doorSecondGameObject.GetComponent<DoorOpener>();
        doorThird = doorThirdGameObject.GetComponent<DoorOpener>();
        
        hexagonChecker = hexagonMinigame.GetComponent<HexagonChecker>();
        hexagonChecker.OnCompletion += OpenHexagonDoor;
    }

    private void OnInteract()
    {
        Debug.Log(pressed + " is interacted");
        if (pressed == required - 1) // La quarta
            pressed++;
    }

    private void OnDisable()
    {
        DisablePlates(platesFirst);
        DisablePlates(platesSecond);
        DisableLevers(leversFirst);
    }

    private void DisablePlates(GameObject[] plates)
    {
        foreach (var plate in plates)
        {
            plate.GetComponent<DetectPressed>().OnPressed -= PlateOnPressed;
            plate.GetComponent<DetectPressed>().OnReleased -= PlateOnReleased;
        }
    }

    private void DisableLevers(GameObject[] levers)
    {
        foreach (var lever in levers)
        {
            lever.GetComponent<LeverOpener>().OnPressed -= PlateOnPressed;
            lever.GetComponent<LeverOpener>().OnReleased -= PlateOnReleased;
            lever.GetComponent<LeverInteract>().OnInteract -= OnInteract;
            lever.GetComponent<InteractReceiver>().enabled = false;
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
        else if (pressed >= required && required == platesSecond.Length && !secondOpened)
        {
            secondOpened = true;
            pressed = 0;
            doorSecond.OpenDoor();
            DisablePlates(platesSecond);
            required = leversFirst.Length;
        }
        else if (pressed >= required && required == leversFirst.Length)
        {
            pressed = 0;
            doorThird.OpenDoor();
            DisableLevers(leversFirst);
            required = 999;
        }
    }
    
    private void OpenHexagonDoor()
    {
        doorHexagon.OpenDoor();
    }
}
