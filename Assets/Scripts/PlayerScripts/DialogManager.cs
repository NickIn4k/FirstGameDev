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
    private readonly string[] lines = { 
        "Lyra: 'Oh, a human! You don’t see many of us around here anymore...'",
        "Lira: 'What are you doing here?'",
        "Lyra: 'My name is Lyra! I’m part of a rebel group fighting against W.R.A.T.H.!'" ,
        "Lyra: 'Judging by your appearence, you must have heard about us..'",
        "Lyra: 'I'm looking for someone to help me find my team..'",
        "Lyra: 'Help me find them! They must be inside that tower down there!'",
        "Lyra: 'To open the door, you need to find the secret code, but don’t worry..'",
        "Lyra: 'Somewhere around here, I left my hacking device'",
        "Lyra: 'It can be used to access the screens and reveal the secret codes...'",
        "Lyra: 'Try looking around, maybe behind this house, or rather, what’s left of it..'",
        "Lyra: 'Show me what you've got! It's your first mission.'"
    };

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
        playerAnimator.SetBool("isInDialog", false);
        DialogUI.SetActive(false);
    }
}
