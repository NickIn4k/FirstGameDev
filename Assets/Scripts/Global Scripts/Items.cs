using UnityEngine;

// Creare una voce personalizzata nel menu di creazione di Unity
// Consente di creare nuove istanze di `Items` direttamente dall'editor Unity
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]

public class Items : ScriptableObject
{
    public int Id;
    public string ItemName;
    public int Value;
    public Sprite Icon;
}
