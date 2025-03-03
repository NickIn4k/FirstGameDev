using UnityEngine;
using TMPro;
using System.Linq;

public class TutorialQuestManager : MonoBehaviour
{
    public TextMeshProUGUI textQuest;
    public GameObject ScreenGlowing;
    public GameObject LockGlowing;
    private int questStep = 0;

    void Start()
    {
        ScreenGlowing.SetActive(false);
        LockGlowing.SetActive(false);
        //Imposta il primo messaggio del tutorial
        ShowQuestMessage();
    }

    void Update()
    {
        //Logica per cambiare i messaggi del tutorial
        if (questStep == 0 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))   //Movimento con WASD
        {
            questStep++;
            ShowQuestMessage();
        }
        else if (questStep == 1 && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))  //Il mouse e' stato mosso
        {
            questStep++;
            ShowQuestMessage();
        }
        else if (questStep == 2 && InventoryManager.Inventory.Any(Items => Items.Id == 1))    //Prendere l'oggetto
        {
            questStep++;
            ShowQuestMessage();
        }
        else if (questStep == 3 && Input.GetKeyDown(KeyCode.Tab))   //Aprire l'inventario con tab
        {
            questStep++;
            ShowQuestMessage();
        }
        else if (questStep == 4)   //Leggere lo schermo con l'oggetto
        {
            questStep++;
            ScreenGlowing.SetActive(true);
            LockGlowing.SetActive(true);
            ShowQuestMessage();
        }
        else if (questStep == 5 && CodeChecker.isOpen)   //Inserire il codice
        {
            questStep++;
            ShowQuestMessage();
        }
    }

    void ShowQuestMessage()
    {
        //Cambia il testo del tutorial
        if (questStep == 0)
            textQuest.text = "Use WASD to move.";
        else if (questStep == 1)
            textQuest.text = "Move the mouse to look around.";
        else if (questStep == 2)
            textQuest.text = "Find and pick up the object by holding 'E' next to it";
        else if (questStep == 3)
            textQuest.text = "Press Tab to open the inventory and click on the hacking device.";
        else if (questStep == 4)
            textQuest.text = "Use your Hacking Device to see what's written inside";
        else if (questStep == 5)
            textQuest.text = "Find the secret code and insert it in the keypad in front of the door";
        else
            textQuest.text = "Tutorial Completed! \nEnter the tower!"; //Messaggio finale
    }
}