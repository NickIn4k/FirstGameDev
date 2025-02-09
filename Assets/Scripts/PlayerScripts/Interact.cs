using UnityEngine;

public class Interact : MonoBehaviour
{
    Rigidbody rb;
    CapsuleCollider cd;
    public LayerMask interactable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cd = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, cd.bounds.size.x * 2f + 0.2f, interactable))
            PlayPopup();
    }

    void PlayPopup()
    {
    }
}
