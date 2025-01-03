using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Manager;
    public List<Items> Inventory;

    public Transform ItemContent;   //Dove vengono inseriri gli items
    public GameObject InventoryItem;    //Riferito all'oggetto UI della lista

    private void Awake()
    {
        Manager = this;
        Inventory = new List<Items>();
    }

    public void Add(Items item)
    {
        Inventory.Add(item);
    }

    public void Remove(Items item)
    {
        Inventory.Remove(item);
    }

    public void ListItems()
    {
        //Pulisci
        foreach(Transform item in ItemContent) 
            Destroy(item.gameObject);

        //Carica
        foreach (Items item in Inventory)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent); //Funzione simile a quella del costruttore del GameObject
            var ItemName = obj.transform.Find("ItemName")?.GetComponent<TMP_Text>();   //Trova l'oggetto ItemName (dalla parte UI)
            var ItemIcon = obj.transform.Find("ItemIcon")?.GetComponent<Image>();
            
            ItemName.text = item.ItemName;
            ItemIcon.sprite = item.Icon;
        }
    }
}
