using System;
using UnityEngine;

public class InteractSender : MonoBehaviour
{
    public CapsuleCollider cc;
    public LayerMask interactible;
    public Transform plr;
    public float viewDistance = 3f;

    public static event EventHandler<InteractArgs> OnShouldPopup;
    public static event Action OnShouldNotPopup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plr = GetComponent<Transform>();
        cc = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(plr.position, plr.forward * 3, Color.red, 1);
        if (Physics.Raycast(plr.position, plr.forward, out hit, cc.bounds.size.x + viewDistance, interactible))
        {
            
            // Notify all that an interactable has been hit
            InteractArgs args = new InteractArgs();
            args.HitTransform = hit.transform;
            OnShouldPopup?.Invoke(this, args);
            Debug.Log("OMGGIGJ");
        } else 
        {
            OnShouldNotPopup?.Invoke();
        }
    }
}
