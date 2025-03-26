using System;
using UnityEngine;

public class LeverInteract : MonoBehaviour
{
    private InteractReceiver interactReceiver;
    public event Action OnInteract;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactReceiver = GetComponent<InteractReceiver>();
        
        interactReceiver.OnInteract += Interact;
    }

    private void Interact()
    {
        if (GetComponent<LeverOpener>().inside == 0)
        {
            OnInteract?.Invoke();
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
