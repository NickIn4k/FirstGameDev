using System.Collections;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public GameObject DialogUI;
    public float detectionRadius;
    public GameObject Player;
    Animator animator;
    Animator playerAnimator;
    private Movement playerMovementScript;

    public TextMeshProUGUI textComponent;
    public float TextSpeed;
    int index;
    public string[] lines;

    private bool isDialogueActive = false;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerMovementScript = player.GetComponent<Movement>();
        playerAnimator = player.GetComponentInChildren<Animator>();
        textComponent.text = string.Empty;
        
        if (gameObject.name == "Lyra")
            animator.SetBool("isLyra", true);
        else
            animator.SetBool("isLyra", false);
    }

    void Update()
    {
        if (player == null || target == null || playerMovementScript == null)
        {
            Debug.LogWarning("Player, target o movement non assegnati!");
            return;
        }

        float distance = Vector3.Distance(player.position, target.position);

        if (distance <= detectionRadius && !isDialogueActive)
            StartCoroutine(StartDialogue());

        if (isDialogueActive && Input.GetMouseButtonDown(0)) // Se clicca, avanza il dialogo
        {
            if (textComponent.text == lines[index])
                NextLine();
            
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index]; // Mostra direttamente tutta la frase
            }
        }
    }

    IEnumerator StartDialogue()
    {
        isDialogueActive = true;
        detectionRadius = -1; // Evita che il dialogo si riattivi

        Debug.Log("Il player è vicino all'oggetto!");
        animator.SetBool("isTalking", true);
        playerAnimator.SetBool("isInDialog", true);
        DialogUI.SetActive(true);
        playerMovementScript.enabled = false;

        index = 0;
        yield return StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = ""; // Pulisce il testo precedente
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(TextSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
            EndDialogue();
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        playerMovementScript.enabled = true;
        animator.SetBool("isTalking", false);
        animator.SetBool("isLyra", false);
        playerAnimator.SetBool("isInDialog", false);
        DialogUI.SetActive(false);
    }
}
