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
    public InteractReceiver ir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        ir = GetComponent<InteractReceiver>();
        ir.OnInteract += () =>
        {
            if (Item.Id == 0 && InventoryManager.Manager.Inventory.Count >= 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Player.SetActive(false);
                Rotator.SetActive(false);
                MainCamera.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Maze.SetActive(true);
                MazePlayer.SetActive(true);
            }
        };
    }
}
