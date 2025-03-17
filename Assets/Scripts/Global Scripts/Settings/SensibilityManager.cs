using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SensibilityManager : MonoBehaviour
{
    GenericSliderManager sensibilityManager;
    [SerializeField] private Slider sensibilitySlider;
    private const string sensibilityKey = "MouseSensitivity";

    void Start()
    {
        sensibilityManager = new GenericSliderManager(sensibilitySlider, sensibilityKey);
        sensibilityManager.SetStart((sensibility) => 
        {
            //Aggiorna la sensibilità globale
            if (GeneralMethods.TryGetRotator(out var rotator))
                rotator.GetComponent<Rotator>().sensitivity = sensibility;
            sensibilityManager.SavePrefab(sensibility);
        });
        float value = sensibilityManager.GetPrefab();
        
        sensibilitySlider.value = value;
        if (GeneralMethods.TryGetRotator(out var rotator))
            rotator.GetComponent<Rotator>().sensitivity = value;
    }
}
