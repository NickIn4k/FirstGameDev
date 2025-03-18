using Settings;
using UnityEngine;

public class ActivateNewCamera : MonoBehaviour
{
    public Items Item;
    public GameObject MainCamera;
    public GameObject Maze;
    public GameObject Rotator;
    public GameObject MazePlayer;
    public GameObject Player;
    public GameObject MazeUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        // CC
        Rotator = GeneralMethods.GetRotator();
        MainCamera = GeneralMethods.GetCamera();
        Player = GeneralMethods.GetPlayer();
        
        CursorSettings.Lock();
        Player.SetActive(false);
        Rotator.SetActive(false);
        MainCamera.SetActive(false);

        MazeUI.SetActive(false);
        CursorSettings.Lock();
        Maze.SetActive(true);
        MazePlayer.SetActive(true);
    }

    public void Close()
    {
        CursorSettings.Lock();
        MazeUI.SetActive(false);
        Player.SetActive(true);
        Rotator.SetActive(true);
        MainCamera.SetActive(true);
    }
}
