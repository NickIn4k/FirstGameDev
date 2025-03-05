using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadInventory : MonoBehaviour
{
    public static LoadInventory Manager;  // Singleton
    static public List<Items> Inventory;

    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject DescriptionUI;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;
    public Image Icon;
    public GameObject ActiveBTN;
    public GameObject UnActiveBTN;

    public Items[] DefaultItems; // Array pubblico di elementi da caricare all'avvio della scena

    private void Awake()
    {
        Manager = this;
        Inventory = new List<Items>();

        if (DefaultItems != null)
        {
            foreach (Items item in DefaultItems)
            {
                Add(item);
            }
        }

        ListItems(); // Aggiorna la UI dopo aver aggiunto gli oggetti iniziali
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
        foreach (Transform item in ItemContent)
            Destroy(item.gameObject);

        foreach (Items item in Inventory)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var ItemName = obj.transform.Find("ItemName")?.GetComponent<TMP_Text>();
            var ItemIcon = obj.transform.Find("ItemIcon")?.GetComponent<Image>();

            ItemName.text = item.ItemName;
            ItemIcon.sprite = item.Icon;

            var button = obj.GetComponent<Button>();
            button.onClick.AddListener(() => ShowDescription(item));
        }
    }

    private void ShowDescription(Items item)
    {
        ActiveBTN.SetActive(false);
        UnActiveBTN.SetActive(false);

        DescriptionUI.SetActive(true);
        NameText.text = item.ItemName;
        Icon.sprite = item.Icon;
        DescriptionText.text = item.Description;

        if(item.isUsable){
            ActiveBTN.SetActive(true);
            UnActiveBTN.SetActive(true);
        }
    }

    public void HideDescription()
    {
        DescriptionUI.SetActive(false);
    }
}