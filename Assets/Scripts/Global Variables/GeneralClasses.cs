using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GenericSliderManager
{
    [SerializeField] private Slider slider;
    #nullable enable
    private string? key; //Chiave per salvare il volume
    #nullable disable
    public GenericSliderManager(Slider slider, string key = null)
    {
        this.slider = slider;
        this.key = key;
    }

    public void SetStart(UnityEngine.Events.UnityAction<float> setParameter, float savedValue = 0.5f){
        //Recupera il valore salvato o usa un valore di default (0.5)
        if (key != null)
            savedValue = PlayerPrefs.GetFloat(key, 0.5f);

        //Imposta il valore dello slider
        if (slider != null)
        {
            slider.value = savedValue;
            slider.onValueChanged.AddListener(setParameter);
        }
    }

    public float GetPrefab(){
        return PlayerPrefs.GetFloat(key, 0.5f);
    }

    public void SavePrefab(float value){
        //Salva il valore nei PlayerPrefs
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }
}
