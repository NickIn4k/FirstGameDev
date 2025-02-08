using UnityEngine;
using UnityEngine.UI;

public class ElectricMinigameManager : MonoBehaviour
{
    //Riferimenti alle Camere
    public Camera mainCamera; //Camera del gioco principale
    public Camera secondaryCamera; //Camera per il minigioco

    //UI Minigame
    public GameObject minigameCanvas; //Canvas con i due pulsanti (in trasparenza)

    //Riferimenti ai Controller
    public ElectricPlayerController playerController; //Script del giocatore
    public ElectricCameraScroller cameraScroller; //Script che muove la camera

    //Posizioni Iniziali (marker nella scena)
    public Transform cameraStartPos; //Punto di partenza per la Secondary Camera
    public Transform playerStartPos; //Punto di partenza per il giocatore

    //Variabili per salvare le posizioni iniziali fisse
    private Vector3 initialCameraPos;
    private Vector3 initialPlayerPos;

    //In Awake memorizziamo le posizioni iniziali dei marker
    void Awake()
    {
        initialCameraPos = cameraStartPos.position;
        initialPlayerPos = playerStartPos.position;
    }

    //Avvio immediato del minigioco per test
    void Start()
    {
       
        
    }

    public void StartMinigame()
    {
        mainCamera.enabled = false;
        minigameCanvas.SetActive(false);

        secondaryCamera.transform.position = initialCameraPos;
        playerController.transform.position = initialPlayerPos;

        secondaryCamera.enabled = true;
        playerController.enabled = true;
        cameraScroller.enabled = true;

        playerController.enabled = true;
        cameraScroller.enabled = true;
    }

    public void OnGameOver()
    {
        playerController.enabled = false;
        cameraScroller.enabled = false;
        minigameCanvas.SetActive(true);
    }
}





/*



using UnityEngine;
using UnityEngine.UI;

public class ElectricMinigameManager : MonoBehaviour
{
    //Riferimenti alle Camere
    public Camera mainCamera; //Camera del gioco principale
    public Camera secondaryCamera; //Camera per il minigioco

    //UI Minigame
    public GameObject minigameCanvas; //Canvas con i due pulsanti (in trasparenza)

    //Riferimenti ai Controller
    public ElectricPlayerController playerController; //Script del giocatore
    public ElectricCameraScroller cameraScroller; //Script che muove la camera

    //Posizioni Iniziali
    public Transform cameraStartPos; //Punto di partenza per la Secondary Camera
    public Transform playerStartPos; //Punto di partenza per il giocatore

    //Inizialmente il minigioco è disattivato
    void Start()
    {
        secondaryCamera.enabled = false;
        minigameCanvas.SetActive(false);
        playerController.enabled = false;
        cameraScroller.enabled = false;
    }

    //Chiamato quando interagisci con lo schermo nel gioco principale
    public void ActivateMinigameUI()
    {
        mainCamera.enabled = false;
        secondaryCamera.enabled = true;
        secondaryCamera.transform.position = cameraStartPos.position;
        playerController.transform.position = playerStartPos.position;
        minigameCanvas.SetActive(true);
    }

    //Chiamato dal pulsante "Start Minigame" del Canvas
    public void StartMinigame()
    {
        minigameCanvas.SetActive(false);
        playerController.enabled = true;
        cameraScroller.enabled = true;
    }

    //Chiamato dal pulsante "Return" del Canvas
    public void ReturnToMainGame()
    {
        playerController.enabled = false;
        cameraScroller.enabled = false;
        minigameCanvas.SetActive(false);
        secondaryCamera.enabled = false;
        mainCamera.enabled = true;
    }

    //Chiamato dallo script del giocatore in caso di collisione con un muro invisibile
    public void OnGameOver()
    {
        playerController.enabled = false;
        cameraScroller.enabled = false;
        minigameCanvas.SetActive(true);
    }
}

*/