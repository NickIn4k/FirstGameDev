using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused;
    public static bool InventoryOn;
    public GameObject PauseMenuUI;
    public GameObject InventoryUI;
    public GameObject QuestUI;
    public GameObject Rotator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GamePaused = false;
        InventoryOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
                Resume(1);
            else
                Pause(1);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (InventoryOn)
                Resume(2);
            else
                Pause(2);
        }

    }
    
    void Resume(int index)
    {
        if(index == 1)
        {
            PauseMenuUI.SetActive(false);
            GamePaused = false;
        }   
        else
        {
            InventoryUI.SetActive(false);
            InventoryOn = false;
        }

        QuestUI.SetActive(true);
        Rotator.SetActive(true);
        Time.timeScale = 1f;
        
    }

    void Pause(int index)
    {
        if (index == 1)
        {
            PauseMenuUI.SetActive(true);
            GamePaused = true;
        }
        else
        {
            InventoryUI.SetActive(true);
            InventoryOn = true;
        }

        QuestUI.SetActive(false);
        Rotator.SetActive(false);
        Time.timeScale = 0f;
    }
}
