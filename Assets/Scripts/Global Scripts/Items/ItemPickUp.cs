using System;
using UnityEngine;

//Gestione di item raccoglibili tramite click del mouse
public class ItemPickUp : MonoBehaviour
{
    Items item;
    InteractReceiver ir;

    private void Start()
    {
        try
        {
            item = GetComponent<ItemManager>().Item;
        }
        catch (NullReferenceException)
        {
            Debug.LogError($"GameObject: {gameObject.name} has no ItemManager. Consider adding one.");
        }
        if (item == null)
            Debug.LogWarning($"GameObject: {gameObject.name} has no Item associated to it");

        try
        {
            ir = GetComponent<InteractReceiver>();
        }
        catch (NullReferenceException) 
        {
            Debug.LogError($"GameObject: {gameObject.name} has no InteractReceiver. Consider adding one.");
        }

        if (ir != null)
            ir.OnInteract += OnInteractHandler;
    }

    private void OnInteractHandler()
    {
        if (InventoryManager.Manager != null && item != null)
            InventoryManager.Manager.Add(item);
        if (LoadInventory.Manager != null && item != null)
            LoadInventory.Manager.Add(item);

        Destroy(gameObject);    //Per eliminare l'item aggiunto
    }
}