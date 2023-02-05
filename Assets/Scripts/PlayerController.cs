using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movimiento
    public float speed = 3.0f;
    // Salto
    public float jumpForce = 250.0f;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    // Sprint
    public float sprintMultiplier = 2.0f;
    public float sprintDuration = 2.0f;
    public float sprintCooldown = 5.0f;

    // Controles
    public KeyCode jumpKey = KeyCode.W;
    public KeyCode jumpKey2 = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public int jugador;


    private Rigidbody2D rigidbody2d;
    private Animator animator;
    // Salto
    private bool facingRight = true;
    private bool isGrounded = false;
    // Sprint
    private float currentSpeed;
    private bool canSprint = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0.0f;

        if (jugador == 1)
        {
            horizontal = Input.GetAxis("Horizontal");
        }
        else if (jugador == 2)
        {
            horizontal = Input.GetAxis("Horizontal2");
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // Salto tocando suelo
        if (isGrounded && (Input.GetKeyDown(jumpKey) || Input.GetKeyDown(jumpKey2)))
        {
            isGrounded = false;
            rigidbody2d.AddForce(new Vector2(0, jumpForce));
        }

        rigidbody2d.velocity = new Vector2(horizontal * currentSpeed, rigidbody2d.velocity.y);

        if (horizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontal < 0 && facingRight)
        {
            Flip();
        }

        // Sprint
        if (Input.GetKeyDown(sprintKey) && canSprint)
        {
            StartCoroutine(Sprint());
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    IEnumerator Sprint()
    {
        canSprint = false;
        currentSpeed = speed * sprintMultiplier;
        yield return new WaitForSeconds(sprintDuration);
        currentSpeed = speed;
        yield return new WaitForSeconds(sprintCooldown);
        canSprint = true;
    }
}