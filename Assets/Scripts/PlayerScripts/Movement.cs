using UnityEngine;
using Settings;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using UnityEditor.Rendering.LookDev;

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

    //public InputActionReference move;
    Inputs inputs;
    InputAction move;

    void Awake()
    {
        inputs = new Inputs();
        rotator = GeneralMethods.GetRotator().transform;
        CursorSettings.Lock();
    }

    private void OnEnable()
    {
        move = inputs.Gameplay.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
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
        rotate();
    }

    private void FixedUpdate() // aggiornamento fisica
    {
        moveCC();
        animate();
        
        if (frames++ % 10 == 0)
            applyDrag();
    }

    void moveCC()
    {
        moveDirection = move.ReadValue<Vector3>();
        moveAnimation = moveDirection;
        moveDirection = rb.rotation * moveDirection;
        rb.AddForce(moveDirection.normalized * Time.deltaTime * 10f * moveSpeed, ForceMode.Force);
    }

    void animate()
    {
        animator.SetBool(GeneralVariables.ISWALKINGFORWARD, moveAnimation.z > 0 && Mathf.Abs(moveAnimation.z) >= Mathf.Abs(moveAnimation.x)); // Prevale
        animator.SetBool(GeneralVariables.ISWALKINGLEFT, moveAnimation.x < 0 && Mathf.Abs(moveAnimation.x) > Mathf.Abs(moveAnimation.z));
        animator.SetBool(GeneralVariables.ISWALKINGBACK, moveAnimation.z < 0 && Mathf.Abs(moveAnimation.z) >= Mathf.Abs(moveAnimation.x)); // Prevale
        animator.SetBool(GeneralVariables.ISWALKINGRIGHT, moveAnimation.x > 0 && Mathf.Abs(moveAnimation.x) > Mathf.Abs(moveAnimation.z));
    }

    void rotate()
    {
        if (moveDirection != Vector3.zero)
        {
            // Calcola l'angolo in base agli input (in gradi)
            float inputAngle = Mathf.Atan2(moveAnimation.x, moveAnimation.y) * Mathf.Rad2Deg;
            // Applica solo una frazione dell'angolo calcolato
            float offset = inputAngle * diagonalRotationFactor;
            
            // Se si sta muovendo, lerp della rotazione verso la direzione della telecamera
            Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, rotator.eulerAngles.y + offset, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulerRotation), rLerp);
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
            rb.linearDamping = 0;     
    }
}
