using System;
using UnityEngine;
using UnityEngine.UI;   //Per gestire i pulsanti

public class HexagonChecker : MonoBehaviour
{
    public GameObject[] hexagons;   //Array degli esagoni da controllare
    public int[] referenceAngles;   //Array degli angoli di riferimento (0-360°)
    public Rotator rotator;
    //public Animator animator;
    //public GameObject Door;
    //public AudioSource Src;
    //public AudioClip Sfx;

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
        OnCompletion?.Invoke();
        //OpenTheDoor();
    }

    /*private void OpenTheDoor()
    {
        //Rende il collider della porta un trigger
        
        if (Door != null)
        {
            Src.clip = Sfx;
            Src.Play();
            Door.SetActive(false);
            animator.SetBool("isOpening", true);
        }
    }*/
}
