using UnityEngine;

//Gestione click degli oggetti non raccoglibili nell'inventario
public class ItemClickerOld : MonoBehaviour
{
    public Items Item;
    public GameObject UI;
    public GameObject QuestUI;
    public GameObject Rotator;

    [Header("Non toccare")]
    public Collider cld; //

    private void OnMouseDown()  //nel click
    {
        if (Item.Id == 0 && InventoryManager.Manager.Inventory.Count > 0)    //Se è presente l'oggetto
        {
            //Disattiva o attiva componenti UI
            QuestUI.SetActive(false);
            Rotator.SetActive(false);
            Time.timeScale = 0f;    //blocco il gioco
            Cursor.lockState = CursorLockMode.None;
            UI.SetActive(true);
            cld.enabled = true;
        }
    }

    public void Resume()
    {
        //Riattiva la grafica di base
        Cursor.lockState = CursorLockMode.Locked;
        UI.SetActive(false);
        QuestUI.SetActive(true);
        Rotator.SetActive(true);
        Time.timeScale = 1f;
        cld.enabled = true;
    }
}