using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SensibilityManager : MonoBehaviour
{
    GenericSliderManager sensibilityManager;
    [SerializeField] private Slider sensibilitySlider;

    void Start()
    {

        sensibilityManager = new GenericSliderManager(sensibilitySlider);
        sensibilityManager.SetStart((sensibility) => 
        {
            //Aggiorna il volume globale
            GeneralVariables.Sensitivity = sensibility;
        });
    }
}
