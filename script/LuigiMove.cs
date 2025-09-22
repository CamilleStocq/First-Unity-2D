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
    private bool isGoingDown = false;

    void Awake()
    {
        xAxis = actions.FindActionMap("Luigi").FindAction("xAxis");
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        actions.FindActionMap("Luigi").Enable();
        actions.FindActionMap("Luigi").FindAction("Jump").performed += OnJump;
        actions.FindActionMap("Luigi").FindAction("Down").performed += GoDown; 
        actions.FindActionMap("Luigi").FindAction("Down").canceled += OnRise; 
    }

    private void OnDisable()
    {
        actions.FindActionMap("Luigi").Disable();
        actions.FindActionMap("Luigi").FindAction("Jump").performed -= OnJump;
        actions.FindActionMap("Luigi").FindAction("Down").performed -= GoDown;
        actions.FindActionMap("Luigi").FindAction("Down").canceled -= OnRise; 
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

    private void GoDown(InputAction.CallbackContext context)
    {
        isGoingDown = true;
        animator.SetBool("GoDown", true);
    }

    private void OnRise(InputAction.CallbackContext context)
    {
        isGoingDown = false;
        animator.SetBool("GoDown", false);
    }

    private void MoveX()
    {
        renderer.flipX = xAxis.ReadValue<float>() < 0;
        // if (isGoingDown) return;  // permet de plus avancer quand on est accroupi

        animator.SetFloat("Speed", Mathf.Abs(xAxis.ReadValue<float>()));
        transform.Translate(xAxis.ReadValue<float>() * speed * Time.deltaTime, 0f, 0f);
    }
}