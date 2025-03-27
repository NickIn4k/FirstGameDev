using UnityEngine;

public class AnimatorControllerSwitcher : MonoBehaviour
{
    // Riferimenti ai controller da assegnare
    [Header("Controller per l'Animator")]
    [Tooltip("Controller da assegnare all'avvio della scena")]
    public RuntimeAnimatorController initialController;

    [Tooltip("Controller da assegnare dopo il completamento dell'azione")]
    public RuntimeAnimatorController newController;

    private Animator animator;

    // Questo metodo viene chiamato all'avvio della scena
    void Start()
    {
        // Recupera il componente Animator dal GameObject a cui è attaccato questo script
        animator = GetComponent<Animator>();

        if (animator != null && initialController != null)
        {
            // Assegna il controller iniziale
            animator.runtimeAnimatorController = initialController;
            Debug.Log("Controller iniziale assegnato.");
        }
        else
        {
            Debug.LogWarning("Animator o Initial Controller non sono stati impostati.");
        }
    }

    // Funzione che cambia il controller dell'Animator dopo il completamento di una certa azione
    public void ChangeAnimatorController()
    {
        if (animator != null && newController != null)
        {
            animator.runtimeAnimatorController = newController;
            Debug.Log("Controller dell'Animator cambiato con successo.");
        }
        else
            Debug.LogWarning("Animator o New Controller non sono stati impostati.");
    }

    public void OnActionCompleted()
    {
        // Puoi inserire qui il codice per verificare il completamento dell'azione desiderata
        // ...
        // Una volta completata l'azione, cambia il controller
        ChangeAnimatorController();
    }
}
