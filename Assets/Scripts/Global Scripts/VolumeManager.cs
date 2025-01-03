using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private const string VolumeKey = "MusicVolume"; //Chiave per salvare il volume

    void Start()
    {
        //Recupera il valore salvato o usa un valore di default (0.5)
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.5f);

        //Imposta il valore dello slider
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        //Applica il volume salvato
        AudioListener.volume = savedVolume;
    }

    public void SetVolume(float volume)
    {
        //Aggiorna il volume globale
        AudioListener.volume = volume;

        //Salva il valore nei PlayerPrefs
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }
}