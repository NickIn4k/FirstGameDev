using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.SceneManagement;
using Settings;

public class VideoManager : MonoBehaviour
{
    [SerializeField] // Consente di rendere pubblica una variabile privata nell'Inspector

    public VideoPlayer videoPlayer; // Assegna il VideoPlayer nel pannello Inspector
    public float delay = 2.0f; // Ritardo in secondi
    public int SceneIndex;
    public bool isLast = false;

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.playOnAwake = false; // Assicurati che Play On Awake sia disattivato
            WaitForSeconds Delay = new WaitForSeconds(delay);
            videoPlayer.Play();
        }
        else
        {
            Debug.LogWarning("VideoPlayer non assegnato!");
        }
        
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        if (isLast)
            CursorSettings.Unlock();

        SceneManager.LoadScene(SceneIndex);
    }
}