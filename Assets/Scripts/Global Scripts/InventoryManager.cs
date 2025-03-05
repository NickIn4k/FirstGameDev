using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Gestore dell'inventario, responsabile di aggiungere, rimuovere e visualizzare gli oggetti
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Manager;     // Singleton (richiama s� stesso)
    static public List<Items> Inventory;               // Items: classe esterna creata su un altro foglio di lavoro 

    public Transform ItemContent;               // Contenitore UI per gli oggetti
    public GameObject InventoryItem;            // Prefab che rappresenta un elemento
    public GameObject DescriptionUI;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;
    public Image Icon;
    public GameObject ActiveBTN;
    public GameObject UnActiveBTN;

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

    public void ListItems()    // Elenca tutti gli oggetti nell'inventario e aggiorna la UI
    {
        // Pulisce il contenitore degli oggetti nella UI
        foreach (Transform item in ItemContent)
            Destroy(item.gameObject);

        // Per ogni oggetto nell'inventario, crea un'istanza nella UI
        foreach (Items item in Inventory)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent); // Nuova istanza del prefab nell'UI
            var ItemName = obj.transform.Find("ItemName")?.GetComponent<TMP_Text>(); // Trova il componente di testo per il nome
            var ItemIcon = obj.transform.Find("ItemIcon")?.GetComponent<Image>();    // Trova il componente immagine per l'icona

            // Imposta il testo e l'icona del nuovo oggetto UI in base ai dati dell'oggetto
            ItemName.text = item.ItemName;
            ItemIcon.sprite = item.Icon;

            // Aggiunge un evento per il mouse hover (OnPointerEnter)
            var button = obj.GetComponent<Button>();
            button.onClick.AddListener(() => ShowDescription(item));
        }
    }

    // Mostra la descrizione dell'oggetto
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

    // Nasconde la descrizione
    public void HideDescription()
    {
        DescriptionUI.SetActive(false);
    }
}
