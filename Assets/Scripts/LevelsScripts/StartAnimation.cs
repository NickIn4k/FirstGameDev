using System;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    public Animator animator;
    InteractReceiver ir;
    public string animationName;
    public AudioSource src;
    public AudioClip clip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        src.clip = clip;
        src.Play();
    }
}
