using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ShowPopup : MonoBehaviour
{
    public Image progressCircle;   //Il cerchio di progresso UI
    public float holdTime = 1f;    //Tempo necessario per saltare

    private float holdDuration; //Contatore per il tempo tenuto premuto
    public Transform lookAt;

    public event Action OnCompletion;

    void Start()
    { 
        holdDuration = 0f;
        if (progressCircle != null)
            progressCircle.fillAmount = 0f;
    }

    void Update()
    {
        transform.LookAt(lookAt);

        //Controlla se il tasto E è premuto
        if (Input.GetKey(KeyCode.E))
        {
            holdDuration += Time.deltaTime;

            //Aggiorna il cerchio di progresso
            if (progressCircle != null)
                progressCircle.fillAmount = holdDuration / holdTime;

            //Se il tempo necessario è raggiunto, DISTRUGGI
            if (holdDuration >= holdTime)
                OnCompletion?.Invoke();
                
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            //Resetta quando il tasto è rilasciato
            holdDuration = 0f;

            if (progressCircle != null)
                progressCircle.fillAmount = 0f;
        }
    }
}

