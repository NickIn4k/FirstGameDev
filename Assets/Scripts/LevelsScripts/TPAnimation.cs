using UnityEngine;

public class TPAnimation : MonoBehaviour
{
    // Trasform del player (assegnabile tramite Inspector)
    public Transform player;

    // Riferimento all'Animator (assegnabile tramite Inspector)
    public Animator animator;

    // Nome del parametro bool nell'Animator da attivare
    public string boolParameterName = "IsNear";

    // Distanza minima per attivare il parametro
    public float activationDistance = 5f;

    public Collider Trigger;

    void Update()
    {
        if (player == null || animator == null)
        {
            Debug.LogWarning("Player o Animator non sono stati assegnati!");
            return;
        }

        // Calcola la distanza tra il player e questo oggetto
        float distance = Vector3.Distance(transform.position, player.position);

        // Se il player è abbastanza vicino, setta il parametro a true, altrimenti a false
        if (distance <= activationDistance)
        {
            animator.SetBool(boolParameterName, true);
            Trigger.isTrigger = true;

            //CODICE DI PASSAGGIO LIVELLO 


        }
        else
        {
            animator.SetBool(boolParameterName, false);
        }
    }
}
