using Unity.VisualScripting;
using UnityEngine;

//Gestione click degli oggetti non raccoglibili nell'inventario
public class ItemClicker : MonoBehaviour
{
    public Items Item;
    public GameObject UI;
    public GameObject QuestUI;
    public GameObject Rotator;

    public InteractReceiver ir;

    private void Start()
    {
        ir = GetComponent<InteractReceiver>();
        ir.OnInteract += () =>
        {
            if (Item.Id == 0 && InventoryManager.Manager.Inventory.Count >= 0)    
            {
                //Disattiva o attiva componenti UI
                if(QuestUI != null) QuestUI.SetActive(false);
                Rotator.SetActive(false);
                GetComponent<MeshCollider>().enabled = false;
                //Time.timeScale = 0f;    //blocco il gioco
                Cursor.lockState = CursorLockMode.None;
                try { UI.GetComponent<ItemClickerOld>().cld = GetComponent<Collider>(); }
                catch { Debug.Log("cld non trovato!"); }
               
                UI.SetActive(true);

            }
        };
    }

    public void Resume()
    {   
        //Riattiva la grafica di base
        Cursor.lockState = CursorLockMode.Locked;
        UI.SetActive(false);
        if(QuestUI != null) QuestUI.SetActive(true);
        Rotator.SetActive(true);
        Time.timeScale = 1f;
        GetComponent<MeshCollider>().enabled = true;
    }
}
