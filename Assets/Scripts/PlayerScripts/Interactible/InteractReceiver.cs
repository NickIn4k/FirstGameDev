using System;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InteractReceiver : MonoBehaviour
{
    bool canPop = false;
    bool canPopAgain = true;
    public Transform lookAt;
    public Canvas canvas;

    Canvas popUp;

    public event Action OnInteract; // evento da mandare ad altri script che dopo ci faranno quello che vogliono

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractSender.OnShouldPopup += ShouldPopupHandler;
        InteractSender.OnShouldNotPopup += ShouldNotPopupHandler;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPop && canPopAgain) 
        {
            canPopAgain = false;

            popUp = Instantiate(canvas);
            popUp.transform.position = 0.5f * (transform.position + lookAt.transform.position) + Vector3.up;
            ShowPopup script = popUp.GetComponent<ShowPopup>();
            //Debug.Log(script.lookAt.position.y);
            //script.lookAt = lookAt;
            script.OnCompletion += Interacted;
            popUp.gameObject.SetActive(true);
        }

        if (popUp != null) 
        {
            popUp.transform.position = 0.5f * (transform.position + lookAt.transform.position) + Vector3.up;
        }
    }

    void Interacted()
    {
        ShouldNotPopupHandler(); // Destroy popup
        OnInteract?.Invoke();
    }

    public void Unsubscribe()
    {
        InteractSender.OnShouldPopup -= ShouldPopupHandler;
        InteractSender.OnShouldNotPopup -= ShouldNotPopupHandler;
    }

    void ShouldPopupHandler (object? sender, InteractArgs args)
    {
        if (args.HitTransform == transform)
        {
            canPop = true;
            //lookAt = (sender as InteractSender).plr;
        }
    }

    void ShouldNotPopupHandler ()
    {
        if (popUp != null)
        {
            canPop = false;
            canPopAgain = true;
            Destroy(popUp.gameObject);
        }
    }
}
