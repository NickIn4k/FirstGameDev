using Settings;
using UnityEngine;

public class Level2CanvasManager : MonoBehaviour
{
    public static bool GamePaused;
    public static bool InventoryOn;
    private bool OnScreen;

    private int active = 0;

    public AudioSource Src;
    public AudioClip sfx;
    public AudioSource WalkSrc;

    // Pannelli della UI
    public GameObject PauseMenuUI;  // Pausa
    public GameObject InventoryUI;  // Inventario
    public GameObject ScreenUI;     // Schermo esterno
    public GameObject DescriptionUI; // Descrizione nell'inventario

    void Start()
    {
        GamePaused = false;
        InventoryOn = false;
        OnScreen = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && active >= 0)   // ESC 
        {
            if (GamePaused)
            {
                active--;
                Resume(1); // (1 = menu di pausa)
            }   
            else
            {
                active++;
                Pause(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) && active <= 0) //TAB
        {
            if (InventoryOn)
            {
                active++;
                Resume(2); // (2 = inventario)
            }
            else
            {
                active--;
                Pause(2);
            }
                
        }
    }

    void Resume(int index)
    {
        Src.clip = sfx;
        Src.Play();
        
        CursorSettings.Lock();

        if (index == 1 && !InventoryOn)
        {
            PauseMenuUI.SetActive(false); // Nascondi pannello
            GamePaused = false;
        }
        else if (index == 2 && !GamePaused)
        {
            InventoryUI.SetActive(false); // Nascondi pannello
            DescriptionUI.SetActive(false);
            InventoryOn = false;
        }

        if (OnScreen)
        {
            ScreenUI.SetActive(true);
            OnScreen = false;
        }

        //Reset della grafica UI e del tempo del gioco
        GeneralMethods.GetRotator().SetActive(true);
        Time.timeScale = 1f;        // Tempo di gioco a velocit� normale
    }

    void Pause(int index)
    {
        WalkSrc.Stop();
        Src.clip=sfx;
        Src.Play();
        CursorSettings.Unlock();
        
        if (index == 1 && !InventoryOn)
        {
            PauseMenuUI.SetActive(true);
            GamePaused = true;
        }
        else if (index == 2 && !GamePaused)
        {
            if (InventoryManager.Manager != null)
                InventoryManager.Manager.ListItems(); // Aggiorna l'inventario 
            else if (LoadInventory.Manager != null) 
                LoadInventory.Manager.ListItems();
            InventoryUI.SetActive(true);
            InventoryOn = true;
        }

        //Disattiva oggetti e blocca il tempo di gioco
        GeneralMethods.GetRotator().SetActive(false);

        if (ScreenUI.activeSelf)
        {
            ScreenUI.SetActive(false);
            OnScreen = true;
        }

        Time.timeScale = 0f;        // Ferma il tempo di gioco
    }

    public void RestartTheLevel(int scene)
    {
        active = 0;
    }
}
