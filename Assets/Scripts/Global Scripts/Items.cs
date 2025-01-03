using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]    //Per creare la parte GUI della folder

public class Items : ScriptableObject
{
    public int Id;
    public string ItemName;
    public int Value;
    public Sprite Icon;
}
