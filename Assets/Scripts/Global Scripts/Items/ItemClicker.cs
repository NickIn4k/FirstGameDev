using Settings;
using Unity.VisualScripting;
using UnityEngine;

//Gestione click degli oggetti non raccoglibili nell'inventario
public class ItemClicker : MonoBehaviour
{
    public Items Item;

    #nullable enable
    public GameObject? UI = null;
    public GameObject? QuestUI = null;


    #nullable disable

    InteractReceiver ir;

    private void Start()
    {
        ir = GetComponent<InteractReceiver>();
        ir.OnInteract += () =>
        {
            if (Item.Id == 0 && (InventoryManager.Inventory?.Count > 0 || LoadInventory.Inventory?.Count > 0))
                GeneralMethods.FreezeGame(UI, QuestUI, GetComponent<MeshCollider>());
        };
    }

    public void Resume()
    {   
        GeneralMethods.ResumeGame(UI, QuestUI, GetComponent<MeshCollider>());
    }
}
