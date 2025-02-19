using System;
using UnityEngine;

public class InteractSender : MonoBehaviour
{
    CapsuleCollider cc;
    Transform plr;

    [Header("Stats")]
    public LayerMask interactible;
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
        Debug.DrawRay(plr.position, plr.forward * 3, Color.red, 1);
        if (Physics.Raycast(plr.position, plr.forward, out var hit, cc.bounds.size.x + viewDistance, interactible))
        {
            // Notify all that an interactable has been hit
            InteractArgs args = new InteractArgs();
            args.HitTransform = hit.transform;
            OnShouldPopup?.Invoke(this, args);

            //Debug.Log("OMGGIGJ"); Non ci credo come ci siano voluti 1 update e 60 patch per rimuoverlo
        } else 
        {
            OnShouldNotPopup?.Invoke();
        }
    }
}
