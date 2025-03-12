using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ElectricManagerV2 : MonoBehaviour
{
    public GameObject mainCamera; //Camera del gioco principale
    public GameObject Game;
    public GameObject secondaryCamera;  //Camera per il minigioco
    public GameObject ForceField;

    // UI Minigame
    public GameObject UI;

    // Riferimenti ai Controller
    public ElectricPlayerController playerController; //Script del giocatore
    public ElectricCameraScroller cameraScroller;     //Script che muove la camera

    public Transform cameraStartPos;  // Punto di partenza per la Secondary Camera
    public Transform playerStartPos;  // Punto di partenza per il giocatore

    // Variabili per salvare le posizioni iniziali fisse
    private Vector3 initialCameraPos;
    private Vector3 initialPlayerPos;
    public GameObject Rotator;
    public GameObject Player;

    public AudioSource Src;
    public AudioClip SfxWin;
    public AudioClip SfxElectricity;

    // Altezza finale per ForceField
    public float forceFieldTargetY = 0f; // Imposta l'altezza desiderata
    public float loweringSpeed = 2f; // Velocit� di abbassamento

    // In Awake memorizziamo le posizioni iniziali
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
        Player.SetActive(false);
        Src.clip = SfxElectricity;
        Src.loop = true;
        Src.Play();
        secondaryCamera.transform.position = initialCameraPos;
        playerController.transform.position = initialPlayerPos;

        // Resetta la lane del player alla lane centrale
        playerController.ResetPlayerLane();
    }

    public void OnGameOver()
    {
        playerController.enabled = false;
        cameraScroller.enabled = false;
        secondaryCamera.SetActive(false);
        Game.SetActive(false);

        mainCamera.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        UI.SetActive(true);
        Player.SetActive(true);
        Src.loop = false;
    }

    public void OnWin()
    {
        playerController.enabled = false;
        cameraScroller.enabled = false;
        Game.SetActive(false);
        mainCamera.SetActive(true);
        secondaryCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        UI.SetActive(false);
        Player.SetActive(true);
        Rotator.SetActive(true);
        Time.timeScale = 1f;

        // Avvia la coroutine per abbassare il ForceField
        StartCoroutine(LowerForceField());

        Src.loop = false;
        Src.clip = SfxWin;
        Src.Play();
    }

    // Coroutine per abbassare il ForceField
    private IEnumerator LowerForceField()
    {
        Vector3 startPos = ForceField.transform.position;
        Vector3 targetPos = new Vector3(startPos.x, forceFieldTargetY, startPos.z);

        while (ForceField.transform.position.y > forceFieldTargetY)
        {
            ForceField.transform.position = Vector3.MoveTowards(ForceField.transform.position, targetPos, loweringSpeed * Time.deltaTime);
            yield return null; // Aspetta il frame successivo prima di continuare
        }
    }
}
