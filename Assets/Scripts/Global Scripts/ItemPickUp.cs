using UnityEngine;

//Gestione di item raccoglibili tramite click del mouse
public class ItemPickUp : MonoBehaviour
{
    public Items Item;

    private void OnMouseDown()
    {
        InventoryManager.Manager.Add(Item);
        Destroy(gameObject);    //Per eliminare l'item aggiunto
    }
}