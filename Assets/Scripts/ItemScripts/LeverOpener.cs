using System;
using UnityEngine;

public class LeverOpener : MonoBehaviour
{
    
    public bool latch = true;
    
    public event Action OnPressed;
    public event Action OnReleased;
    
    public int inside = 0;
    
    Collider[] colliders;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (latch && inside > 0)
        {
            OnPressed?.Invoke();
            latch = false;
        }
        else if (!latch && inside == 0)
        {
            OnReleased?.Invoke();
            latch = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        inside++;
    }

    void OnTriggerExit(Collider other)
    {
        inside--;
    }
}
