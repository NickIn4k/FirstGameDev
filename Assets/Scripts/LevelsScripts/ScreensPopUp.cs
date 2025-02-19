using UnityEngine;

public class ScreensPopUp : MonoBehaviour
{
    public GameObject UI;
    InteractReceiver ir;

    private void Start()
    {
        ir = GetComponent<InteractReceiver>();
        ir.OnInteract += () =>
        {
            GeneralMethods.FreezeGame(UI);
        };
    }

    public void Resume()
    {
        GeneralMethods.ResumeGame(UI);
    }
}
