using System.Linq;
using TMPro;
using UnityEngine;

public class Lvl3TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI textQuest;
    public GameObject questCanva;
    private int questStep = 0;
    private bool hasSelectedNPC = false; //Controlla se è stato premuto un tasto da 1 a 4
    private bool hasSelectedNPCForInteraction = false; //Controlla se è stato premuto un tasto da 1 a 3

    void Start()
    {
        //Imposta il primo messaggio del tutorial
        ShowQuestMessage();
    }

    void Update()
    {
        //Controlla se è stato selezionato un NPC (tasto da 1 a 4)
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            hasSelectedNPC = true;
        }

        //Controlla se è stato selezionato un NPC solo con tasti da 1 a 3
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            hasSelectedNPCForInteraction = true;
        }

        //Logica per cambiare i messaggi del tutorial
        if (questStep == 0 && hasSelectedNPC) //Selezionare un NPC
        {
            questStep++;
            ShowQuestMessage();
        }
        else if (questStep == 1 && Input.GetKeyDown(KeyCode.E)) //Premere 'E'
        {
            questStep++;
            ShowQuestMessage();
        }
        else if (questStep == 2 && Input.GetKeyDown(KeyCode.Mouse0)) //Confermare lo spostamento
        {
            questStep++;
            hasSelectedNPC = false;
            hasSelectedNPCForInteraction = false;
            ShowQuestMessage();
        }
        else if (questStep == 3 && hasSelectedNPC && Input.GetKeyDown(KeyCode.R)) //Farsi seguire
        {
            questStep++;
            hasSelectedNPC = false;
            hasSelectedNPCForInteraction = false;
            ShowQuestMessage();
        }
        else if (questStep == 4 && hasSelectedNPCForInteraction && Input.GetKeyDown(KeyCode.T)) //Fare interagire l'NPC
        {
            questStep++;
            ShowQuestMessage();
        }
        else if (questStep == 5 && Input.GetKeyDown(KeyCode.Mouse0)) //Confermare la scelta
        {
            questStep++;
            ShowQuestMessage();
        }
    }

    void ShowQuestMessage()
    {
        //Cambia il testo del tutorial
        if (questStep == 0)
            textQuest.text = "Use the associated numbered key shown in the bottom right image to select the NPC you want to command";
        else if (questStep == 1)
            textQuest.text = "Press 'E' to tell the NPC where to go";
        else if (questStep == 2)
            textQuest.text = "Press left mouse button to confirm where you want the NPC to go";
        else if (questStep == 3)
            textQuest.text = "Otherwise, if you need them to follow you, you can just press 'R'";
        else if (questStep == 4)
            textQuest.text = "But if you need them to interact with something, like levers, you can press 'T'";
        else if (questStep == 5)
            textQuest.text = "Hover an obect with the cursor after pressing 'T' with an NPC to interact with it";
        else
            questCanva.SetActive(false);
    }
}
