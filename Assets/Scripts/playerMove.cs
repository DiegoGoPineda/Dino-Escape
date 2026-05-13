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

    private bool recibiendoDanio;
    private bool atacando;
    public float fuerzaRebote = 20f;

    public int vida = 3;
    public bool muerto;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void Update()
{
    if(!muerto)
    {
        CheckFlip();
        UpdateGroundedState();
        animator.SetFloat("Horizontal", Mathf.Abs(HorizontalMovement));
        animator.SetBool("IsGrounded", IsGrounded);
        animator.SetBool("Damage", recibiendoDanio);
        animator.SetBool("Atacando", atacando);
    }
    
    // ¡Esta línea debe ir afuera para que el animator sepa que el jugador murió!
    animator.SetBool("muerto", muerto); 
}

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2D.linearVelocity = new Vector2(HorizontalMovement * moveSpeed, rb2D.linearVelocity.y);
    }

   public void Move(InputAction.CallbackContext context)
{
    if(muerto) return; // Si está muerto, ignora el mando
    HorizontalMovement = context.ReadValue<Vector2>().x;
}

    public void Jump(InputAction.CallbackContext context)
{
    if(muerto || !context.performed || !IsGrounded) return; // Se añade la validación aquí
    animator.SetTrigger("Jump");
    rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpHeight);
}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }

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

   public void RecibeDanio(Vector2 direction, int cantDanio)
    {
        if (!recibiendoDanio)
        {
            recibiendoDanio = true;
            vida -= cantDanio;

            if (vida <= 0)
            {
                muerto = true;
                
                // --- CORRECCIONES PARA DETENER AL PERSONAJE ---
                HorizontalMovement = 0; // Evita que el input siga registrando movimiento
                rb2D.linearVelocity = Vector2.zero; // Frena en seco cualquier inercia o física activa
                // ----------------------------------------------

                // Aquí puedes agregar la lógica para manejar la muerte del jugador, como reproducir una animación de muerte o reiniciar el nivel.
            }

            // 1. Calculamos la distancia para saber de qué lado nos golpeó
            float distanciaX = transform.position.x - direction.x;
            
            // 2. Por defecto, asumimos que saldremos disparados hacia la derecha (1)
            float empujeX = 1f; 
            
            // 3. Si estábamos a la izquierda del enemigo, salimos disparados a la izquierda (-1)
            if (distanciaX < 0) 
            {
                empujeX = -1f;
            }

            if (!muerto)
            {
                // 4. Creamos un vector fijo usando nuestro empujeX y un 1 en Y
                Vector2 rebote = new Vector2(empujeX, 1).normalized;
                
                rb2D.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }
        }
    }

    public void ResetDamage(){
        recibiendoDanio = false;
    }

    public void DesactivaDanio(){
        recibiendoDanio = false;
        rb2D.linearVelocity = Vector2.zero;
    }
    

    public void Ataca(InputAction.CallbackContext context)
    {
    if(muerto) return; // No puede atacar si está muerto
    if(context.performed && !atacando){
        atacando = true;
    }
    }
   
   public void DesactivarAtaque(){
        atacando = false;
    }
}
