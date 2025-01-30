using Settings;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public Transform player;  // Riferimento al player
    public Transform target;  // Riferimento all'oggetto da controllare
    public GameObject DialogUI;
    public float detectionRadius; // Distanza minima per considerare il player vicino
    public GameObject Player;

    Animator animator;
    Animator playerAnimator;

    private Movement playerMovementScript;
    private string[] frasi = {"A", "AA", "AAA" };

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerMovementScript = player.GetComponent<Movement>();
        playerAnimator = player.GetComponentInChildren<Animator>();
    }

    async void Update()
    {
        if (player == null || target == null || playerMovementScript == null)
        {
            Debug.LogWarning("Player, target o movement non assegnati!");
            return;
        }

        float distance = Vector3.Distance(player.position, target.position);

        if (distance <= detectionRadius)
        {
            detectionRadius = -1;
            Debug.Log("Il player è vicino all'oggetto!");

            animator.SetBool("isTalking", true);
            playerAnimator.SetBool("isInDialog", true);
            DialogUI.SetActive(true);

            playerMovementScript.enabled = false;

            await ShowTexts();

            playerMovementScript.enabled = true;
            animator.SetBool("isTalking", false);
            playerAnimator.SetBool("isInDialog", false);
            DialogUI.SetActive(false);
        }
    }
    private async Task ShowTexts()
    {
        for(int i=0; i<frasi.Length; i++)
        {
            await Task.Delay(1000);
            Debug.Log(frasi[i]);
            //Cambia testo dentro al panel
        }
    }
}
