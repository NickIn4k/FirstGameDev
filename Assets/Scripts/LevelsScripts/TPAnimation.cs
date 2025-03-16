using UnityEngine;

public class TPAnimation : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public string boolParameterName = "IsNear";
    public float activationDistance = 5f;
    public Collider Trigger;
    public AudioSource src;
    public AudioClip clip;

    private bool previousState = false;

    void Update()
    {
        if (player == null || animator == null || src == null || clip == null)
        {
            Debug.LogWarning("Player, Animator o AudioSource non assegnati!");
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position); //Calcola la distanza tra il player e questo oggetto
        bool isNear = distance <= activationDistance; //Determina se il player è vicino

        if (isNear != previousState) //Controlla se lo stato è cambiato
        {
            animator.SetBool(boolParameterName, isNear);
            Trigger.isTrigger = isNear;
            src.PlayOneShot(clip); //Riproduce il suono ogni volta che cambia stato
        }

        previousState = isNear; //Aggiorna lo stato precedente
    }
}




/*

using UnityEngine;



public class TPAnimation : MonoBehaviour
{
    //Trasform del player (assegnabile tramite Inspector)
    public Transform player;

    //Riferimento all'Animator (assegnabile tramite Inspector)
    public Animator animator;

    //Nome del parametro bool nell'Animator da attivare
    public string boolParameterName = "IsNear";

    //Distanza minima per attivare il parametro
    public float activationDistance = 5f;

    public Collider Trigger;

    public AudioSource src;
    public AudioClip clip;

    void Update()
    {
        if (player == null || animator == null)
        {
            Debug.LogWarning("Player o Animator non sono stati assegnati!");
            return;
        }

        //Calcola la distanza tra il player e questo oggetto
        float distance = Vector3.Distance(transform.position, player.position);

        //Se il player è abbastanza vicino, setta il parametro a true, altrimenti a false
        if (distance <= activationDistance)
        {
            animator.SetBool(boolParameterName, true);
            Trigger.isTrigger = true;

            src.clip = clip;
            src.Play();
        }
        else
        {
            animator.SetBool(boolParameterName, false);
        }
    }
}
*/