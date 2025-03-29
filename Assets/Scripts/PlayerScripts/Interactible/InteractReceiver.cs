using System;
using UnityEngine;
//using static UnityEditor.Progress;

public class InteractReceiver : MonoBehaviour
{
    bool canPop = false;
    public bool canPopAgain = true;
    public bool interactible = true;
    Transform lookAt;
    GameObject canvasGameObject;
    Canvas canvas;

    Canvas popUp;

    public event Action OnInteract; // evento da mandare ad altri script che dopo ci faranno quello che vogliono

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGameObject = GeneralMethods.GetInteractCanvas();
        canvas = canvasGameObject.GetComponent<Canvas>();

        lookAt = GeneralMethods.GetCamera().GetComponentsInChildren<Transform>()[1];

        InteractSender.OnShouldPopup += ShouldPopupHandler;
        InteractSender.OnShouldNotPopup += ShouldNotPopupHandler;

        if (gameObject.layer != LayerMask.NameToLayer("interactible"))
            Debug.LogWarning($"GameObject: {gameObject.name} has no interactible layer. Consider setting it.");
    }

    // Update is called once per frame
    void Update()
    {
        if (interactible)
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

            if (popUp) 
            {
                Vector3 targetPosition = 0.5f * (transform.position + lookAt.transform.position) + Vector3.up;
                targetPosition.y -= 0.4f; // offset � il valore per abbassare il popup
                popUp.transform.position = targetPosition;
            }
        }
    }

    private void OnDestroy()
    {
        Unsubscribe(); // Do not touch
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

    void ShouldPopupHandler(object sender, InteractArgs args)
    {
        if (args.HitTransform == transform)
        {
            canPop = true;
            //lookAt = (sender as InteractSender).plr;
        }
    }

    void ShouldNotPopupHandler()
    {
        if (popUp != null)
        {
            canPop = false;
            canPopAgain = true;
            Destroy(popUp.gameObject);
        }
    }
}
