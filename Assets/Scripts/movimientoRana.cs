using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class movimientoRana : MonoBehaviour
{
    public float runSpeed= 2;
    public float jumpSpeed = 3;
    Rigidbody2D rb2d;

    // salto mejorada mas papu pro
    public bool betterJumping = true;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey("d")||Input.GetKey("right"))
        {
            rb2d.linearVelocity = new Vector2(runSpeed, rb2d.linearVelocity.y); // Movimiento a la derecha
            spriteRenderer.flipX = false; // Voltear el sprite a la derecha
            animator.SetBool("Correr", true);
        }
        else if(Input.GetKey("a")||Input.GetKey("left"))
        {
            rb2d.linearVelocity = new Vector2(-runSpeed, rb2d.linearVelocity.y); // Movimiento a la izquierda
            spriteRenderer.flipX = true; // Voltear el sprite a la izquierda
            animator.SetBool("Correr", true);
            
        }
        else
        {
            rb2d.linearVelocity = new Vector2(0, rb2d.linearVelocity.y); // quieto
            animator.SetBool("Correr", false);  
        }
        if(Input.GetKey("space") && CheckPiso.isGrounded)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed); // Salto
            animator.SetBool("Correr", false);
        } 
        if(CheckPiso.isGrounded==false)
        {
            animator.SetBool("Salto", true);
            animator.SetBool("Correr", false);
        }
        if(CheckPiso.isGrounded==true)
        {
            animator.SetBool("Salto", false);
        }
        if (betterJumping)
        {
            if (rb2d.linearVelocity.y < 0)
            {
                rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier  ) * Time.deltaTime;
            }
            else if (rb2d.linearVelocity.y > 0 && !Input.GetKey("space"))
            {
                rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }
    }
}
