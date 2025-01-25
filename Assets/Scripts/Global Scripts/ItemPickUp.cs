using UnityEngine;

//Gestione di item raccoglibili tramite click del mouse
public class ItemPickUp : MonoBehaviour
{
    public Items Item;
    InteractReceiver ir;

    private void Start()
    {
        ir = GetComponent<InteractReceiver>();
        ir.OnInteract += OnInteractHandler;
    }

    private void OnInteractHandler()
    {
        ir.Unsubscribe();
        ir.OnInteract -= OnInteractHandler;
        InventoryManager.Manager.Add(Item);
        Destroy(gameObject);    //Per eliminare l'item aggiunto
    }
}