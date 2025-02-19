using UnityEngine;

public class GroundPhysics : MonoBehaviour
{

    Rigidbody rb;

    bool isGrounded;
    public LayerMask ground;

    [Header("Stats")]
    public float groundDrag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
