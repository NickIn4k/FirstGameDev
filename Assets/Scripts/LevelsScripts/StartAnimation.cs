using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class StartAnimation : MonoBehaviour
{
    //interact
    InteractReceiver ir;

    //animation
    public Animator animator;
    public string animationName;

    //sfx
    public AudioSource src;
    public AudioClip clip_leva;
    public AudioClip clip_corrente;

    //altro
    public GameObject? luce;  //La luce da disattivare
    public int prossimaScena;  //L'indice della scena successiva

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            ir = GetComponent<InteractReceiver>();
        }
        catch (NullReferenceException)
        {
            Debug.LogError($"GameObject: {gameObject.name} has no InteractReceiver. Consider adding one.");
        }

        if (ir != null)
            ir.OnInteract += OnInteractHandler;
    }

    public void OnInteractHandler()
    {
        Debug.Log("Server 1 shutdown");
        animator.SetBool(animationName, true);
        src.clip = clip_leva;
        src.Play();

        if (luce != null)
            luce.SetActive(false);

        GetComponent<InteractReceiver>().enabled = false;

        src.PlayOneShot(clip_corrente);

        StartCoroutine(CambioLivello());
    }

    public IEnumerator CambioLivello()
    {
        //Aspetta 1 secondo prima di cambiare scena
        yield return new WaitForSeconds(3f);

        //Cambia scena
        if(prossimaScena != -1)
            SceneManager.LoadScene(prossimaScena);
    }
}
