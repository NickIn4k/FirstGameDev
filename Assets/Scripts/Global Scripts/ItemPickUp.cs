using UnityEngine;

//Gestione di item raccoglibili tramite click del mouse
public class ItemPickUp : MonoBehaviour
{
    public Items Item;
    InteractReceiver ir;

    private void Start()
    {
        ir = GetComponent<InteractReceiver>();
        if (ir != null)
            ir.OnInteract += OnInteractHandler;
        else
            Debug.Log("Oggetto InteractReceiver non trovato: Non posso completare il livello");
    }

    private void OnInteractHandler()
    {
        ir.OnInteract -= OnInteractHandler;
        ir.Unsubscribe();

        if (InventoryManager.Manager != null)
            InventoryManager.Manager.Add(Item);

        Destroy(gameObject);    //Per eliminare l'item aggiunto
    }
}