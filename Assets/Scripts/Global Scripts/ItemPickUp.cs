using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Items Item;

    void PickUp()
    {
        InventoryManager.Manager.Add(Item);
        Destroy(gameObject);    //Per eliminare l'item aggiunto
    }

    private void OnMouseDown()
    {
        PickUp();
    }
}