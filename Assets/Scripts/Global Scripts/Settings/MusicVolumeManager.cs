using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
//TEST DEL CODICE PER IL VOLUME DELLA MUSICA
public class MusicVolumeManager : MonoBehaviour
{
    GenericSliderManager volumeManager;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource[] audioSources;
    private const string VolumeKey = "AudioSourceVolume"; //Chiave per salvare il volume dell'AudioSource

    void Start()
    {
        volumeManager = new GenericSliderManager(volumeSlider, VolumeKey);
        volumeManager.SetStart((volume) =>
        {
            //Aggiorna il volume del componente AudioSource
            foreach(AudioSource audio in audioSources)
                audio.volume = volume;
            volumeManager.SavePrefab(volume);
        });
        //Imposta il volume iniziale recuperato
        foreach (AudioSource audio in audioSources)
            audio.volume = volumeManager.GetPrefab();
    }
}
