using UnityEngine;

//Gestione click degli oggetti non raccoglibili nell'inventario
public class ItemClicker : MonoBehaviour
{
    public Items Item;
    public GameObject UI;
    public GameObject QuestUI;
    public GameObject Rotator;

    private void OnMouseDown()  //nel click
    {
        if(Item.Id == 0 && InventoryManager.Manager.Inventory.Count > 0)    //Se è presente l'oggetto
        {
            //Disattiva o attiva componenti UI
            QuestUI.SetActive(false);   
            Rotator.SetActive(false);
            Time.timeScale = 0f;    //blocco il gioco
            UI.SetActive(true);
        }
    }

    public void Resume()
    {   
        //Riattiva la grafica di base
        UI.SetActive(false);
        QuestUI.SetActive(true);
        Rotator.SetActive(true);
        Time.timeScale = 1f;
    }
}
