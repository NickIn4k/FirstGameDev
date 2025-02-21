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
            GeneralMethods.GetRotator().GetComponent<Rotator>().Sensitivity = sensibility;
            sensibilityManager.SavePrefab(sensibility);
        });
        GeneralMethods.GetRotator().GetComponent<Rotator>().Sensitivity = sensibilityManager.GetPrefab();
    }
}
