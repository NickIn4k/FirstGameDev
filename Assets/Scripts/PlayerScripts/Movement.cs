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

    public LayerMask ground;

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

        Vector3 move = new Vector3(vertical, 0f, -horizontal);
        move *= Time.deltaTime * 10;

        if (move != Vector3.zero)
        {
            // if moving rotate player to camera forward
            Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, cam.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Euler(eulerRotation);

            // movement
            transform.Translate(move, Space.Self);
        }




        // jumping
        if (Input.GetKey(KeyCode.Space) && isGrounded && !debounce)
        {
            //debounce = true;
            //jump();
            //debounceTimer.Start();
        }

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
