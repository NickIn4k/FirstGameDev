using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused;  
    public static bool InventoryOn;
    private bool OnScreen;
    private bool OnCode;

    // Pannelli della UI
    public GameObject PauseMenuUI;  // Pausa
    public GameObject InventoryUI;  // Inventario
    public GameObject QuestUI;      // Missioni (quests)
    public GameObject ScreenUI;     // Schermo esterno
    public GameObject CodeUI;       // Schermo di codice
    public GameObject Rotator;      // Rotator della Main Camera

    void Start()
    {
        GamePaused = false;
        InventoryOn = false;
        OnScreen = false;
        OnCode = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // ESC 
        {
            if (GamePaused)
                Resume(1); // (1 = menu di pausa)
            else
                Pause(1); 
        }

        if (Input.GetKeyDown(KeyCode.Tab)) //TAB
        {
            if (InventoryOn)
                Resume(2); // (2 = inventario)
            else
                Pause(2);
        }
    }

    void Resume(int index)
    {
        if (index == 1 && !InventoryOn) 
        {
            PauseMenuUI.SetActive(false); // Nascondi pannello
            GamePaused = false;
        }
        else if (index == 2 && !GamePaused)
        {
            InventoryUI.SetActive(false); // Nascondi pannello 
            InventoryOn = false;
        }

        if(OnScreen)
        {
            ScreenUI.SetActive(true);
            OnScreen = false;
        }

        if (OnCode)
        {
            CodeUI.SetActive(true);
            OnCode = false;
        } 

        //Reset della grafica UI e del tempo del gioco
        QuestUI.SetActive(true);    
        Rotator.SetActive(true);    
        Time.timeScale = 1f;        // Tempo di gioco a velocità normale
    }

    void Pause(int index)
    {
        if (index == 1 && !InventoryOn) 
        {
            PauseMenuUI.SetActive(true); 
            GamePaused = true;
        }
        else if (index == 2 && !GamePaused) 
        {
            InventoryManager.Manager.ListItems(); // Aggiorna l'inventario 
            InventoryUI.SetActive(true);        
            InventoryOn = true;
        }

        //Disattiva oggetti e blocca il tempo di gioco
        QuestUI.SetActive(false);   
        Rotator.SetActive(false);   
        
        if(ScreenUI.activeSelf)
        {
            ScreenUI.SetActive(false);
            OnScreen = true;
        }
        if (CodeUI.activeSelf)
        {
            CodeUI.SetActive(false);
            OnCode = true;
        }
            
        Time.timeScale = 0f;        // Ferma il tempo di gioco
    }
}
