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
    public bool CanPlayAgain = false;

    private bool isDialogueActive = false;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private Coroutine forceIdleCoroutine;

    private void Start()
    {
        // CC
        Player = GeneralMethods.GetPlayer();
        player = Player.transform;
        
        
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

        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            // Se il testo è in fase di digitazione, ferma solo quella coroutine e completa il testo
            if (isTyping)
            {
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                    typingCoroutine = null;
                }
                textComponent.text = lines[index];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    IEnumerator StartDialogue()
    {
        isDialogueActive = true;
        detectionRadius = -1; // Evita che il dialogo si riattivi
        Debug.Log("Il player è vicino all'oggetto!");

        // Disabilita il movement script prima di forzare le animazioni
        playerMovementScript.enabled = false;

        // Avvia la coroutine per forzare l'animazione idle durante il dialogo
        forceIdleCoroutine = StartCoroutine(ForceIdleAnimation());

        // Imposta gli altri parametri per il dialogo
        animator.SetBool("isTalking", true);
        playerAnimator.SetBool("isInDialog", true);
        DialogUI.SetActive(true);

        index = 0;
        typingCoroutine = StartCoroutine(TypeLine());
        yield return null;
    }

    IEnumerator ForceIdleAnimation()
    {
        while (isDialogueActive)
        {
            // Assicura che il player non mostri animazioni di camminata
            playerAnimator.SetBool("isWalkingForward", false);
            // Forza l'Idle: attenzione, se chiamato ogni frame riavvia l'animazione; tuttavia se l'Idle è looping non è un problema
            playerAnimator.Play("Idle", 0, 0f);
            yield return null;
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = "";
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(TextSpeed);
        }
        isTyping = false;
        typingCoroutine = null;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        // Fermiamo la coroutine che forzava l'Idle
        if (forceIdleCoroutine != null)
        {
            StopCoroutine(forceIdleCoroutine);
            forceIdleCoroutine = null;
        }

        playerMovementScript.enabled = true;
        animator.SetBool("isTalking", false);
        animator.SetBool("isLyra", false);
        playerAnimator.SetBool("isInDialog", false);
        DialogUI.SetActive(false);

        // Al termine, garantiamo una volta sola il passaggio all'Idle
        playerAnimator.Play("Idle", 0, 0f);

        if (CanPlayAgain)
            StartCoroutine(ResetDetectionRadius());
    }

    IEnumerator ResetDetectionRadius()
    {
        yield return new WaitForSeconds(3f);
        detectionRadius = 4.5f;
    }
}
