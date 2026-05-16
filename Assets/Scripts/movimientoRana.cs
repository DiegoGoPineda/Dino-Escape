using UnityEngine;
using System.Collections;

public class movimientoRana : MonoBehaviour
{
    [Header("Movimiento Base")]
    public float runSpeed = 2f;
    public float jumpSpeed = 3f;
    private Rigidbody2D rb2d;

    [Header("Salto Pro (Físicas Mejoradas)")]
    public bool betterJumping = true;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Componentes Visuales")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [Header("Sistema de Estado y Vidas")]
    public int vida = 3;
    public int vidaMax = 3;
    public bool muerto = false;
    private bool recibiendoDanio = false;
    public float fuerzaRebote = 12f; // Ajustado para el peso de tu rana

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        vida = vidaMax;
        muerto = false;
        recibiendoDanio = false;
    }

    void Update()
    {
        // Si el personaje está muerto, bloqueamos por completo las animaciones y acciones de movimiento
        if (muerto) return;

        // Control de Animaciones de Salto y Caída basadas en el script original de la rana
        if (CheckPiso.isGrounded == false)
        {
            animator.SetBool("Salto", true);
            animator.SetBool("Correr", false);
        }
        else
        {
            animator.SetBool("Salto", false);
        }
    }

    void FixedUpdate()
    {
        // Si está muerto o recuperándose de un golpe (recibiendoDanio), las físicas del golpe toman el control
        if (muerto || recibiendoDanio) return;

        // --- MOVIMIENTO HORIZONTAL CLÁSICO ---
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.linearVelocity = new Vector2(runSpeed, rb2d.linearVelocity.y);
            spriteRenderer.flipX = false;
            animator.SetBool("Correr", true);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2d.linearVelocity = new Vector2(-runSpeed, rb2d.linearVelocity.y);
            spriteRenderer.flipX = true;
            animator.SetBool("Correr", true);
        }
        else
        {
            rb2d.linearVelocity = new Vector2(0, rb2d.linearVelocity.y);
            animator.SetBool("Correr", false);
        }

        // --- SISTEMA DE SALTO ---
        if (Input.GetKey("space") && CheckPiso.isGrounded)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed);
            animator.SetBool("Correr", false);
        }

        // --- SISTEMA BETTER JUMPING ---
        if (betterJumping)
        {
            if (rb2d.linearVelocity.y < 0)
            {
                rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            else if (rb2d.linearVelocity.y > 0 && !Input.GetKey("space"))
            {
                rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }
    }

    // --- INTERFAZ DE DAÑO (Llamada por Mobs y Púas) ---
    public void RecibeDanio(Vector2 direction, int cantDanio)
    {
        if (muerto || recibiendoDanio) return;

        recibiendoDanio = true;
        vida -= cantDanio;

        // Comportamiento al perder todas las vidas
        if (vida <= 0)
        {
            muerto = true;
            rb2d.linearVelocity = Vector2.zero;
            animator.SetBool("Correr", false);
            animator.SetBool("Salto", false);

            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
            return;
        }

        // --- CÁLCULO FÍSICO DEL REBOTE (KNOCKBACK) ---
        float distanciaX = transform.position.x - direction.x;
        float empujeX = (distanciaX < 0) ? -1f : 1f;

        // Limpiamos velocidad actual para que el golpe se sienta limpio
        rb2d.linearVelocity = Vector2.zero;

        // Aplicamos la fuerza de rebote (empuje horizontal y un pequeño salto vertical hacia arriba)
        Vector2 vectorRebote = new Vector2(empujeX, 1.2f).normalized;
        rb2d.AddForce(vectorRebote * fuerzaRebote, ForceMode2D.Impulse);

        // Iniciamos el parpadeo visual y recuperación
        StartCoroutine(TiempoRecuperacion());
    }

    IEnumerator TiempoRecuperacion()
    {
        // Efecto visual de parpadeo básico usando el SpriteRenderer alpha
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f); // Transparente
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white; // Normal
            yield return new WaitForSeconds(0.1f);
        }

        recibiendoDanio = false;
    }

    public void CurarVida(int cantidad)
    {
        if (muerto) return;
        vida += cantidad;
        if (vida > vidaMax)
        {
            vida = vidaMax;
        }
    }
}