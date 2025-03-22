using System;
using UnityEngine;

public class DetectPressed : MonoBehaviour
{
    private BoxCollider boxCollider;

    public bool latch = true;
    
    public event Action OnPressed;
    public event Action OnReleased;
    
    private int inside = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
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
