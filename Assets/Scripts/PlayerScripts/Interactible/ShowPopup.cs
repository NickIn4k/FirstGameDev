using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ShowPopup : MonoBehaviour
{
    public Image progressCircle;   //Il cerchio di progresso UI
    public float holdTime = 1f;    //Tempo necessario per saltare

    private float holdDuration; //Contatore per il tempo tenuto premuto
    Transform lookAt;

    private Inputs inputs;
    private InputAction interactAction;
    
    private bool isInteracting;
    
    public event Action OnCompletion;

    void Start()
    { 
        lookAt = GeneralMethods.GetCamera().GetComponentsInChildren<Transform>()[1];
        holdDuration = 0f;
        if (progressCircle)
            progressCircle.fillAmount = 0f;
    }

    void OnEnable()
    {
        inputs = new Inputs();
        interactAction = inputs.Gameplay.Interact;
        interactAction.Enable();

        interactAction.performed += Interacting;
        interactAction.canceled += CanceledInteraction;
    }

    void OnDisable()
    {
        interactAction.performed -= Interacting;
        interactAction.canceled -= CanceledInteraction;
        
        inputs.Disable();
        interactAction.Disable();
    }
    
    private void Interacting(InputAction.CallbackContext obj)
    {
        isInteracting = true;
    }
    
    private void CanceledInteraction(InputAction.CallbackContext obj)
    {
        isInteracting = false;
    }

    void Update()
    {
        transform.LookAt(lookAt);

        if (isInteracting)
        {
            holdDuration += Time.deltaTime;

            //Aggiorna il cerchio di progresso
            if (progressCircle)
                progressCircle.fillAmount = holdDuration / holdTime;

            //Se il tempo necessario viene raggiunto, distruggi l'oggetto
            if (holdDuration >= holdTime)
                OnCompletion?.Invoke();
        }
        else
        {
            // Reset
            holdDuration = 0f;

            if (progressCircle)
                progressCircle.fillAmount = 0f;
        }
    }
}

