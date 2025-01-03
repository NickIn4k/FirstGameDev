using UnityEngine;
using TMPro;

public class TutorialQuestManager : MonoBehaviour
{
    public TextMeshProUGUI textQuest;
    private int questStep = 0;

    void Start()
    {
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
        else if (questStep == 1 && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))  //Il mouse è stato mosso
        {
            questStep++;
            ShowQuestMessage();
        }
        else if (questStep == 2 && InventoryManager.Manager.Inventory.Count > 0)    //Prendere l'oggetto
        {
            questStep++;
            ShowQuestMessage();
        }
        else if (questStep == 3 && Input.GetKeyDown(KeyCode.Tab))   //Aprire l'inventario con tab
        {
            questStep++;
            ShowQuestMessage();
        }
        else if (questStep == 4)   //Leggere Lo schermo con l'oggetto
        {
            questStep++;
            ShowQuestMessage();
        }
        else if (questStep == 5 && !CodeChecker.isOpen)   //Inserire il codice
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
            textQuest.text = "Pick up the objects by left-clicking it.";
        else if (questStep == 3)
            textQuest.text = "Press Tab to open the inventory.";
        else if (questStep == 4)
            textQuest.text = "Use your Hacking Device to see what's written inside";
        else if (questStep == 5)
            textQuest.text = "Insert the code in the keypad and open the tower's door";
        else
            textQuest.text = "Tutorial Completed! \nEnter the tower!"; //Messaggio finale
    }
}