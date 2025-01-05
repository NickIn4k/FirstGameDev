using UnityEngine;
using System.Timers;
using Unity.VisualScripting;

public class Movement : MonoBehaviour
{
    bool isGrounded;
    bool debounce = false;
    System.Timers.Timer debounceTimer;
    Rigidbody rb;
    public Transform cam;

    public float rLerp = .075f; // Speed of easing

    public LayerMask ground;
    Vector3 move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        checkGrounded();

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        move = new Vector3(vertical, 0f, -horizontal);

        if (move != Vector3.zero) 
        {
            // if moving lerp player rotation to camera forward
            Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, cam.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulerRotation), rLerp);
        }

        if (transform.position.y < 0)
            this.transform.position = new Vector3(0,0,0);

        // jumping
        if (Input.GetKey(KeyCode.Space) && isGrounded && !debounce)
        {
            //debounce = true;
            //jump();
            //debounceTimer.Start();
        }

    }

    private void FixedUpdate()
    {
        // movement
        move = rb.rotation * move;
        rb.AddForce(move * Time.deltaTime * 100, ForceMode.Force);
    }

    void jump()
    {
        isGrounded = false;
        rb.AddForce(Vector3.up * 200);
    }

    void checkGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, GetComponent<CapsuleCollider>().bounds.size.y * 0.5f + 0.2f, ground))
            isGrounded = true;
        else
            isGrounded = false;
    }
}
