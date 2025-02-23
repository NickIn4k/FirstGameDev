using UnityEngine;
using UnityEngine.UI;
//TEST DEL CODICE PER IL VOLUME DELLA MUSICA
public class AudioSourceVolumeManager : MonoBehaviour
{
    GenericSliderManager volumeManager;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource audioSource;
    private const string VolumeKey = "AudioSourceVolume"; // Chiave per salvare il volume dell'AudioSource

    void Start()
    {
        volumeManager = new GenericSliderManager(volumeSlider, VolumeKey);
        volumeManager.SetStart((volume) =>
        {
            // Aggiorna il volume del componente AudioSource
            audioSource.volume = volume;
            volumeManager.SavePrefab(volume);
        });
        // Imposta il volume iniziale recuperato
        audioSource.volume = volumeManager.GetPrefab();
    }
}
