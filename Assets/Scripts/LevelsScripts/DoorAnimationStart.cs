using UnityEngine;

public class DoorAnimationStart : MonoBehaviour
{
    public Animator animator;  // Riferimento all'Animator
    public string animationTrigger = "PlayAnimation";  // Nome del parametro trigger nell'Animator
    public float triggerDistance = 5f;  // Distanza minima per attivare l'animazione
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // Trova il player tramite tag
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= triggerDistance)
            {
                animator.SetTrigger(animationTrigger);
            }
        }
    }
}

