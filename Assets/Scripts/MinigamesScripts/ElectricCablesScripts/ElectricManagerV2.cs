using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Settings;

public class ElectricManagerV2 : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject Game;
    public GameObject secondaryCamera;
    public GameObject ForceField;
    
    // UI Minigame
    public GameObject UI;

    // Riferimenti ai Controller
    public ElectricPlayerController playerController;
    public ElectricCameraScroller cameraScroller;

    public Transform cameraStartPos;
    public Transform playerStartPos;

    private Vector3 initialCameraPos;
    private Vector3 initialPlayerPos;
    public GameObject Rotator;
    public GameObject Player;

    public AudioSource Src;
    public AudioClip SfxWin;
    public AudioClip SfxElectricity;

    // Tempo (in secondi) tra ogni riduzione dell'1%
    public float shrinkDelay = 0.1f; 

    void Awake()
    {
        initialCameraPos = cameraStartPos.position;
        initialPlayerPos = playerStartPos.position;
    }

    public void StartMinigame()
    {
        CursorSettings.Lock();
        
        // CC
        mainCamera = GeneralMethods.GetCamera();
        Rotator = GeneralMethods.GetRotator();
        Player = GeneralMethods.GetPlayer();
        
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
        playerController.ResetPlayerLane();
    }

    public void OnGameOver()
    {
        playerController.enabled = false;
        cameraScroller.enabled = false;
        secondaryCamera.SetActive(false); 
        mainCamera.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        UI.SetActive(true);
        Player.SetActive(true);
        Src.loop = false;
        Game.SetActive(false);
    }

    public async Task OnWin()
    {
        playerController.enabled = false;
        cameraScroller.enabled = false;
        mainCamera.SetActive(true);
        secondaryCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        UI.SetActive(false);
        Player.SetActive(true);
        Rotator.SetActive(true);
        Time.timeScale = 1f;

        Src.loop = false;
        Src.clip = SfxWin;
        Src.Play();
        //StartCoroutine(ShrinkAndDisableForceField());
        await StartProcess();
        Game.SetActive(false);

    }

    private IEnumerator ShrinkAndDisableForceField()
    {
        // Recupera il collider (sia esso MeshCollider, BoxCollider, etc.)
        Collider forceFieldCollider = ForceField.GetComponent<Collider>();

        // Salva la scala originale
        Vector3 originalScale = ForceField.transform.localScale;
        float currentPercent = 1f;

        // Riduci la scala dell'1% della scala originale ogni shrinkDelay secondi
        while (currentPercent > 0f)
        {
            currentPercent -= 0.003f;
            if (currentPercent < 0f)
                currentPercent = 0f;
            
            // Applica la nuova scala mantenendo le proporzioni originali
            ForceField.transform.localScale = originalScale * currentPercent;
            yield return new WaitForSeconds(shrinkDelay);
        }

        // Disattiva il collider, se presente
        if (forceFieldCollider != null)
        {
            forceFieldCollider.enabled = false;
        }
        // Disattiva il GameObject una volta che la scala è zero
        ForceField.SetActive(false);
    }

    private async Task StartProcess()
    {
        await ShrinkAndDisableForceFieldAsync();
    }

    private async Task ShrinkAndDisableForceFieldAsync()
    {
        var tcs = new TaskCompletionSource<bool>();
        StartCoroutine(ShrinkCoroutine(tcs));
        await tcs.Task;
    }

    private IEnumerator ShrinkCoroutine(TaskCompletionSource<bool> tcs)
    {
        yield return StartCoroutine(ShrinkAndDisableForceField());
        tcs.SetResult(true);
    }
}
