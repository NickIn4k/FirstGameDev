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

    public AudioSource? Src;
    public AudioClip? Sfx;

    public bool canInteractAgain = true;

    #nullable disable

    InteractReceiver ir;

    public SphereCollider sphere;

    private void Start()
    {
        sphere = GetComponent<SphereCollider>();

        ir = GetComponent<InteractReceiver>();
        ir.OnInteract += () =>
        {
            if (Item.Id == 0 && (InventoryManager.Inventory?.Count > 0 || LoadInventory.Inventory?.Count > 0))
                GeneralMethods.FreezeGame(UI, QuestUI, sphere != null ? sphere : GetComponent<MeshCollider>());
            Src.clip = Sfx;
            Src.Play();
        };
        
    }

    public void Resume()
    {   
        GeneralMethods.ResumeGame(UI, QuestUI, canInteractAgain ? sphere != null ? sphere : GetComponent<MeshCollider>() : null);

        if (canInteractAgain)
        {
            GetComponent<InteractReceiver>().interactible = true;
            GetComponent<InteractReceiver>().canPopAgain = true;
        }
    }
}
