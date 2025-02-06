using UnityEngine;

public class ScreensPopUp : MonoBehaviour
{
    public GameObject UI;
    public GameObject Rotator;

    public InteractReceiver ir;

    private void Start()
    {
        ir = GetComponent<InteractReceiver>();
        ir.OnInteract += () =>
        {
            Rotator.SetActive(false);
            GetComponent<MeshCollider>().enabled = false;
            Time.timeScale = 0f;    //blocco il gioco
            Cursor.lockState = CursorLockMode.None;
            UI.GetComponent<ItemClickerOld>().cld = GetComponent<Collider>();
            UI.SetActive(true);
        };
    }

    public void Resume()
    {
        //Riattiva la grafica di base
        Cursor.lockState = CursorLockMode.Locked;
        UI.SetActive(false);
        Rotator.SetActive(true);
        Time.timeScale = 1f;
        GetComponent<MeshCollider>().enabled = true;
    }
}
