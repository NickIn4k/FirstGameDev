using UnityEngine;
using Settings;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    Transform rotator;

    [Header("Movement Speed")]
    public float moveSpeed = 7.5f;
    public float groundDrag;

    [Header("Rotation Speed")]
    public float rLerp = .075f; // Speed of easing

    [Header("Diagonal Rotation Factor")]
    public float diagonalRotationFactor = 0.2f;

    Vector3 moveDirection;
    Vector3 moveAnimation;

    Animator animator;

    private int frames = 0;

    // Audio
    [Header("Audio Settings")]
    public AudioSource mainSrc;           // AudioSource per altri suoni
    public AudioSource footstepsSrc;      // AudioSource dedicato per i passi
    public AudioClip clip;                // Clip per il suono dei passi

    // Inputs
    Inputs inputs;
    InputAction move;
    InputAction run;

    private bool isRunning = false;

    void Awake()
    {
        inputs = new Inputs();
        rotator = GeneralMethods.GetRotator().transform;
        CursorSettings.Lock();
    }

    private void OnEnable()
    {
        inputs.Enable();
        move = inputs.Gameplay.Move;
        run = inputs.Gameplay.Run;
        move.Enable();
        run.Enable();
        run.performed += StartedRunning;
        run.canceled += StoppedRunning;
    }

    private void OnDisable()
    {
        move.Disable();
        run.Disable();
        run.performed -= StartedRunning;
        run.canceled -= StoppedRunning;
        inputs.Disable();
    }

    private void StartedRunning(InputAction.CallbackContext obj)
    {
        isRunning = true;
        moveSpeed = 120f;
    }

    private void StoppedRunning(InputAction.CallbackContext obj)
    {
        isRunning = false;
        moveSpeed = 60f;
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Imposta la clip sull'AudioSource dei passi se non � gi� impostata
        if (footstepsSrc != null && clip != null)
        {
            footstepsSrc.clip = clip;
            footstepsSrc.loop = true; // assicura la ripetizione finch� il giocatore si muove
        }
    }

    void Update()
    {
        rotate();
    }

    private void FixedUpdate() // aggiornamento fisica
    {
        moveCC();
        animate();

        // Gestione audio per i passi usando footstepsSrc
        if (moveAnimation.magnitude > 0.1f)
        {
            if (footstepsSrc != null && !footstepsSrc.isPlaying)
            {
                footstepsSrc.Play();
            }
        }
        else
        {
            if (footstepsSrc != null && footstepsSrc.isPlaying)
            {
                footstepsSrc.Stop();
            }
        }

        if (frames++ % 10 == 0)
            applyDrag();
    }

    void moveCC()
    {
        if (!isRunning)
        {
            moveDirection = move.ReadValue<Vector3>();
        }
        else
        {
            moveDirection = new Vector3(0, 0, move.ReadValue<Vector3>().z > 0 ? move.ReadValue<Vector3>().z : 0);
        }

        moveAnimation = moveDirection;
        moveDirection = rb.rotation * moveDirection;

        rb.AddForce(moveDirection.normalized * Time.deltaTime * 10f * moveSpeed, ForceMode.Force);
    }

    void animate()
    {
        animator.SetBool(GeneralVariables.ISWALKINGFORWARD, moveAnimation.z > 0 && Mathf.Abs(moveAnimation.z) >= Mathf.Abs(moveAnimation.x) && !isRunning);
        animator.SetBool(GeneralVariables.ISWALKINGLEFT, moveAnimation.x < 0 && Mathf.Abs(moveAnimation.x) > Mathf.Abs(moveAnimation.z) && !isRunning);
        animator.SetBool(GeneralVariables.ISWALKINGBACK, moveAnimation.z < 0 && Mathf.Abs(moveAnimation.z) >= Mathf.Abs(moveAnimation.x) && !isRunning);
        animator.SetBool(GeneralVariables.ISWALKINGRIGHT, moveAnimation.x > 0 && Mathf.Abs(moveAnimation.x) > Mathf.Abs(moveAnimation.z) && !isRunning);
        animator.SetBool(GeneralVariables.ISRUNNING, moveAnimation.z > 0 && isRunning);
    }

    void rotate()
    {
        if (moveDirection != Vector3.zero)
        {
            GeneralVariables.isCCMoving = true;

            float inputAngle = Mathf.Atan2(moveAnimation.x, moveAnimation.y) * Mathf.Rad2Deg;
            float offset = inputAngle * diagonalRotationFactor;

            Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, rotator.eulerAngles.y + offset, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulerRotation), rLerp);
        }
        else if (GeneralVariables.isCCMoving)
        {
            GeneralVariables.isCCMoving = false;
        }
    }

    void applyDrag()
    {
        CapsuleCollider cc = gameObject.GetComponent<CapsuleCollider>();
        if (Physics.Raycast(cc.bounds.center, Vector3.down, cc.bounds.size.y * 0.5f + 0.2f, GeneralVariables.GROUND))
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
    }
}
