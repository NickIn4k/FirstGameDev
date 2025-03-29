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
            Debug.Log("Pressed");
            latch = false;
        }
        else if (!latch && inside == 0)
        {
            OnReleased?.Invoke();
            Debug.Log("Released");
            latch = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("NPC"))
            inside++;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("NPC"))
            inside--;
    }
}
