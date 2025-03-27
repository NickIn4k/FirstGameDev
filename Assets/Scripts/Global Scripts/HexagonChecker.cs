using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   //Per gestire i pulsanti

public class HexagonChecker : MonoBehaviour
{
    public GameObject[] hexagons;   //Array degli esagoni da controllare
    public int[] referenceAngles;   //Array degli angoli di riferimento (0-360°)
    public Rotator rotator;
    public GameObject interactibleScreen;
    public Animator animator;
    public GameObject Door;
    public AudioSource Src;
    public AudioClip Sfx;

    public event Action OnCompletion;

    public void CheckRotations()
    {
        if (hexagons.Length != referenceAngles.Length)
        {
            Debug.LogError("Gli array non hanno la stessa lunghezza!");
            return;
        }

        for (int i = 0; i < hexagons.Length; i++)
        {
            float hexRotation = Mathf.Round(hexagons[i].transform.eulerAngles.z) % 360; //Rotazione attuale
            int refRotation = referenceAngles[i] % 360; //Rotazione di riferimento

            if (hexRotation != refRotation)
            {
                Debug.Log("Le rotazioni non corrispondono");
                return;
            }
        }

        Debug.Log("Tutti gli esagoni sono nella posizione corretta!");

        //Rende lo schermo non più interagibile
        if (interactibleScreen != null)
            interactibleScreen.GetComponent<InteractReceiver>().canPopAgain = false;

        OnCompletion?.Invoke();

        //CODICE PROVVISORIO PER DEBUG --> Se il livello è il livello 2 (scena 4) apre la porta
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 4)
            OpenTheDoor();
    }

    private void OpenTheDoor()
    {
        //Rende il collider della porta un trigger
        
        if (Door != null)
        {
            Src.clip = Sfx;
            Src.Play();
            Door.SetActive(false);
            animator.SetBool("isOpening", true);
        }
    }
}
