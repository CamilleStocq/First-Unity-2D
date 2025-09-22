using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]


public class LuigiMove : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 200f;

    private InputAction xAxis;
    private SpriteRenderer renderer;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isJumping = false;
    private bool goingDown = false; // exo

    void Awake()
    {
        xAxis = actions.FindActionMap("Luigi").FindAction("xAxis");
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // boxCollider = GetComponent<BoxCollider2D>(); // exo
    }

    private void OnEnable()
    {
        actions.FindActionMap("Luigi").Enable();
        actions.FindActionMap("Luigi").FindAction("Jump").performed += OnJump;
        actions.FindActionMap("Luigi").FindAction("Down").performed += GoDown; //exo
    }

    private void OnDisable()
    {
        actions.FindActionMap("Luigi").Disable();
        actions.FindActionMap("Luigi").FindAction("Jump").performed -= OnJump;
        actions.FindActionMap("Luigi").FindAction("Down").performed -= GoDown;//exo
    }

    void Update()
    {
        MoveX();

        if (isJumping)
        {
            if (rb.linearVelocityY < 0)
            {
                isJumping = false;
                animator.SetBool("OnJump", false);
            }
        }
    }
    
     private void OnJump(InputAction.CallbackContext context)
    {
        if (!isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce);
            animator.SetBool("OnJump", true);
            isJumping = true;
        }
    }

    private void GoDown(InputAction.CallbackContext context) //exo
    {
        if (!goingDown)
        {
            animator.SetBool("GoDown", true);
            goingDown = true;
            // boxCollider.size = new Vector3(boxCollider.size.x, ySize);
        }
    }

    private void MoveX()
    {
        renderer.flipX = xAxis.ReadValue<float>() < 0;
        animator.SetFloat("Speed", Mathf.Abs(xAxis.ReadValue<float>()));
        transform.Translate(xAxis.ReadValue<float>() * speed * Time.deltaTime, 0f, 0f);
    }
}