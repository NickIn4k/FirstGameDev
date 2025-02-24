using UnityEngine;
using Settings;

public class Movement : MonoBehaviour
{
    bool isGrounded;

    Rigidbody rb;
    Transform rotator;

    [Header("Movement Speed")]
    public float moveSpeed = 10f;
    public float groundDrag;

    [Header("Rotation Speed")]
    public float rLerp = .075f; // Speed of easing

    [Header("What is ground")]
    public LayerMask ground;

    // Fattore che controlla l'impatto dell'offset di rotazione in movimento diagonale (0 = nessun offset, 1 = offset completo)
    [Header("Diagonal Rotation Factor")]
    public float diagonalRotationFactor = 0.2f;

    Vector3 move;

    float horizontalInput;
    float verticalInput;

    Animator animator;

    void Awake()
    {
        rotator = GeneralMethods.GetRotator().transform;
        CursorSettings.Lock();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        checkGrounded();

        if (isGrounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        getInput();

        // Calcola l'ampiezza degli input per ciascun asse
        float absVertical = Mathf.Abs(verticalInput);
        float absHorizontal = Mathf.Abs(horizontalInput);

        // Resetta tutti i booleani prima
        animator.SetBool("isWalkingForward", false);
        animator.SetBool("isWalkingBack", false);
        animator.SetBool("isWalkingLeft", false);
        animator.SetBool("isWalkingRight", false);

        // Priorità alla camminata laterale:
        if (absHorizontal > 0)
        {
            if (horizontalInput < 0)
                animator.SetBool("isWalkingLeft", true);
            else if (horizontalInput > 0)
                animator.SetBool("isWalkingRight", true);
        }
        // Se non c'è input orizzontale, gestisci l'input verticale.
        else if (absVertical > 0)
        {
            if (verticalInput > 0)
                animator.SetBool("isWalkingForward", true);
            else if (verticalInput < 0)
                animator.SetBool("isWalkingBack", true);
        }

        // Gestione della rotazione in base al movimento:
        if (move != Vector3.zero)
        {
            // Se il movimento è diagonale, applica un offset ridotto
            if (absHorizontal > 0 && absVertical > 0)
            {
                // Calcola l'angolo in base agli input (in gradi)
                float angle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
                // Applica solo una frazione dell'angolo calcolato
                float smallOffset = angle * diagonalRotationFactor;
                Vector3 targetEuler = new Vector3(transform.eulerAngles.x, rotator.eulerAngles.y + smallOffset, transform.eulerAngles.z);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetEuler), rLerp);
            }
            else
            {
                // Movimento non diagonale: usa la rotazione verso la telecamera
                Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, rotator.eulerAngles.y, transform.eulerAngles.z);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulerRotation), rLerp);
            }
        }

        if (transform.position.y < 0)
            transform.position = new Vector3(0, 4, 0);
    }

    private void FixedUpdate() // aggiornamento fisica
    {
        getMovement();

        // movement
        move = rb.rotation * move;
        rb.AddForce(move.normalized * Time.deltaTime * 10f * moveSpeed, ForceMode.Force);
    }

    void getInput()
    {
        // inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void getMovement()
    {
        // movement direction
        move = new Vector3(horizontalInput, 0f, verticalInput);
    }

    void checkGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, GetComponent<CapsuleCollider>().bounds.size.y * 0.5f + 0.2f, ground))
            isGrounded = true;
        else
            isGrounded = false;
    }
}
