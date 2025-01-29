using UnityEngine;
using Settings;

public class Movement : MonoBehaviour
{
    bool isGrounded;

    public float groundDrag;

    Rigidbody rb;

    public Transform cam;

    [Header("Movement Speed")]
    public float moveSpeed = 10f;

    [Header("Rotation Speed")]
    public float rLerp = .075f; // Speed of easing

    public LayerMask ground;
    Vector3 move;

    float horizontalInput;
    float verticalInput;

    Animator animator;

    void Awake()
    { 
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

        // Controlla se il giocatore si sta muovendo avanti
        if (verticalInput > 0)
        {
            animator.SetBool("isWalkingForward", true);
            animator.SetBool("isWalkingBack", false);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
        }
        else if(verticalInput < 0 )
        {
            animator.SetBool("isWalkingBack", true);
            animator.SetBool("isWalkingForward", false);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
        }
        else if(horizontalInput < 0)
        {
            animator.SetBool("isWalkingLeft", true);
            animator.SetBool("isWalkingForward", false);
            animator.SetBool("isWalkingBack", false);
            animator.SetBool("isWalkingRight", false);
        }
        else if(horizontalInput > 0)
        {
            animator.SetBool("isWalkingRight", true);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingForward", false);
            animator.SetBool("isWalkingBack", false);
        }
        else
        {
            animator.SetBool("isWalkingForward", false);
            animator.SetBool("isWalkingBack", false);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
        }

        if (move != Vector3.zero)
        {
            // Se si sta muovendo, lerp della rotazione verso la direzione della telecamera
            Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, cam.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulerRotation), rLerp);
        }

        if (transform.position.y < 0)
            this.transform.position = new Vector3(0, 4, 0);
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
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
    }
}
