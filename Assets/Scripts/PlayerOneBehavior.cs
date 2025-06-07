using UnityEngine;

public class PlayerOneBehavior : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D coll;

    [SerializeField] LayerMask groundLayer;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.1f;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        if (groundCheck == null)
        {
            Debug.LogError("Please assign a GroundCheck transform in the inspector.");
        }
    }


    void Update()
    {
        MovimentHandler();

    }

    public void MovimentHandler()
    {
        float moveX = 0f;


        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }

        Vector2 velocity = rb.linearVelocity;
        velocity.x = moveX * moveSpeed;
        rb.linearVelocity = velocity;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
