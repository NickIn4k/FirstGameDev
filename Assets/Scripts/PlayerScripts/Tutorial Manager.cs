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
        if (questStep == 0 && Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            questStep = 1;
            ShowQuestMessage();
        }
        else if (questStep == 1 && Input.GetKeyDown(KeyCode.Mouse0))    //MODIFICARE
        {
            questStep = 2;
            ShowQuestMessage();
        }
        else if (questStep == 2 && Input.GetKeyDown(KeyCode.Tab))
        {
            questStep = 3;
            ShowQuestMessage();
        }
    }

    void ShowQuestMessage()
    {
        // Cambia il testo in base al passo del tutorial
        if (questStep == 0)
            textQuest.text = "Use WASD to move.";
        else if (questStep == 1)
            textQuest.text = "Move the mouse to look around.";
        else if (questStep == 2)
            textQuest.text = "Press Tab to open the inventory.";
        else
            textQuest.text = "Tutorial Completed!";  // Messaggio finale
    }
}
