using UnityEngine;
using UnityEngine.InputSystem;

public class playerMove : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float moveSpeed = 5f;
    float HorizontalMovement;
    float VerticalMovement;

    public float jumpHeight = 5f;

    public Transform groundCheck;
    public Vector2 groundCheckSize = new Vector2(0.25f, 0.25f);
    public LayerMask groundLayer;
    private bool IsGrounded;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        CheckFlip();
        UpdateGroundedState();
        animator.SetFloat("Horizontal", Mathf.Abs(HorizontalMovement));
        animator.SetBool("IsGrounded", IsGrounded);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2D.linearVelocity = new Vector2(HorizontalMovement * moveSpeed, rb2D.linearVelocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        HorizontalMovement = context.ReadValue<Vector2>().x;

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(!context.performed || !IsGrounded)
            return;
            animator.SetTrigger("Jump");
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpHeight);
        

    }
/*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
*/
    void UpdateGroundedState()
    {
        IsGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
    }

    void CheckFlip(){
        if(HorizontalMovement==0)
            return;

        bool movingRight = HorizontalMovement > 0;
        bool facingRight = transform.localScale.x > 0;

        if(movingRight != facingRight)
        {
            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }
    }

   
}
