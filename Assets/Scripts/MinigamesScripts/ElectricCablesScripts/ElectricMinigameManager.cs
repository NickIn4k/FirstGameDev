using UnityEngine;
using UnityEngine.UI;

public class ElectricMinigameManager : MonoBehaviour
{
    //Riferimenti alle Camere
    public GameObject mainCamera; //Camera del gioco principale
    public GameObject Game;       //Camera per il minigioco
    public GameObject secondaryCamera;

    //UI Minigame
    public GameObject UI;         //Canvas con i due pulsanti (in trasparenza)

    //Riferimenti ai Controller
    public ElectricPlayerController playerController; //Script del giocatore
    public ElectricCameraScroller cameraScroller;       //Script che muove la camera

    //Posizioni Iniziali (marker nella scena)
    public Transform cameraStartPos;  //Punto di partenza per la Secondary Camera
    public Transform playerStartPos;  //Punto di partenza per il giocatore

    //Variabili per salvare le posizioni iniziali fisse
    private Vector3 initialCameraPos;
    private Vector3 initialPlayerPos;

    //In Awake memorizziamo le posizioni iniziali dei marker
    void Awake()
    {
        initialCameraPos = cameraStartPos.position;
        initialPlayerPos = playerStartPos.position;
    }

    public void StartMinigame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        secondaryCamera.SetActive(true);
        Game.SetActive(true);
        mainCamera.SetActive(false);
        UI.SetActive(false);
        playerController.enabled = true;
        cameraScroller.enabled = true;
        
        secondaryCamera.transform.position = initialCameraPos;
        playerController.transform.position = initialPlayerPos;
        
        //Resetta la lane del player alla lane centrale
        playerController.ResetPlayerLane();
    }

    public void OnGameOver()
    {
        playerController.enabled = false;
        cameraScroller.enabled = false;
        Game.SetActive(false);
        mainCamera.SetActive(true);
        secondaryCamera.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        UI.SetActive(true);
    }
}
