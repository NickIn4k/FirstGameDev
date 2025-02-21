using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    GenericSliderManager volumeManager;
    [SerializeField] private Slider volumeSlider;
    private const string VolumeKey = "MusicVolume"; //Chiave per salvare il volume

    void Start()
    {
        volumeManager = new GenericSliderManager(volumeSlider, VolumeKey);
        volumeManager.SetStart((volume) => 
        {
            //Aggiorna il volume globale
            AudioListener.volume = volume;
            volumeManager.SavePrefab(volume);
        });
        AudioListener.volume = volumeManager.GetPrefab();
    }
}