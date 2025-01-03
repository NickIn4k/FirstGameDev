using UnityEngine;

public class ItemClicker : MonoBehaviour
{
    public Items Item;
    public GameObject UI;
    public GameObject QuestUI;
    public GameObject Rotator;

    private void OnMouseDown()
    {
        if(Item.Id == 0)
        {
            QuestUI.SetActive(false);
            Rotator.SetActive(false);
            Time.timeScale = 0f;
            UI.SetActive(true);
        }
    }

    public void Resume()
    {
        UI.SetActive(false);
        QuestUI.SetActive(true);
        Rotator.SetActive(true);
        Time.timeScale = 1f;
    }
}
