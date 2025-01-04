using UnityEngine;
using UnityEngine.Video;
using System.Collections; 

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assegna il VideoPlayer nel pannello Inspector
    public float delay = 2.0f; // Ritardo in secondi

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
    }
}