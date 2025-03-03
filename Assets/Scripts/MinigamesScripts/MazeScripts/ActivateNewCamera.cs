using UnityEngine;
using static UnityEditor.Progress;

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
        Cursor.lockState = CursorLockMode.Locked;
        Player.SetActive(false);
        Rotator.SetActive(false);
        MainCamera.SetActive(false);

        Maze.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Maze.SetActive(true);
        MazePlayer.SetActive(true);
    }

    public void Close()
    {

    }
}
