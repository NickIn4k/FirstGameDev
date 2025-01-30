using Settings;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //Controlla se è il player a toccare il trigger
        {
            //AGGIUNGERE ANIMAZIONE E DIALOGO
            Debug.Log("DIALOGO");
        }
    }
}
