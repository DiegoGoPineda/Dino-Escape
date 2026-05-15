using UnityEngine;
using System.Collections;
public class EnemyControler : MonoBehaviour
{

    public Transform player;
    public float speed = 2.0f;
    public float detectionRadius = 5.0f;
    public int vida = 3;
    private Rigidbody2D rb2D;
    private float movementX;

    private bool enMovimiento;

    private Animator animator;
    private bool recibiendoDanio;
    public float fuerzaRebote = 5f;
    private bool muerto;
    private bool  playerVivo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerVivo = true;
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
   // Update is called once per frame
    void Update()
    {
        if (playerVivo && !muerto)
        {
            Movimiento();
        }
        animator.SetBool("enMovimiento", enMovimiento);
        animator.SetBool("muerto", muerto);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          Vector2 direccionDanio = new Vector2(transform.position.x,0);
          playerMove playerScript = collision.gameObject.GetComponent<playerMove>();
            playerScript.RecibeDanio(direccionDanio, 1);
            playerVivo= !playerScript.muerto;
            if(!playerVivo){
               enMovimiento = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Espada"))
        {
          Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x,0);
            RecibeDanio(direccionDanio, 1);
        }
    }

    public void RecibeDanio(Vector2 direction, int cantDanio){
        if(!recibiendoDanio){
            vida -= cantDanio;
            recibiendoDanio = true;
            
            if(vida <= 0)
            {
                muerto = true;
                enMovimiento = false;
                
                // --- LA SOLUCIÓN ---
                // Frena en seco cualquier inercia o rebote que tuviera el enemigo
                rb2D.linearVelocity = Vector2.zero; 
                // -------------------
            }
            else
            {
                // 1. Calculamos la distancia para saber de qué lado nos golpeó
                float distanciaX = transform.position.x - direction.x;
                
                // 2. Por defecto, asumimos que saldremos disparados hacia la derecha (1)
                float empujeX = 1f; 
                
                // 3. Si estábamos a la izquierda del enemigo, salimos disparados a la izquierda (-1)
                if (distanciaX < 0) {
                    empujeX = -1f;
                }

                // 4. Creamos un vector fijo usando nuestro empujeX y un 1 en Y
                Vector2 rebote = new Vector2(empujeX, 1).normalized;
                rb2D.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
                
                StartCoroutine(DesactivaDanio());
            }
        }
    }
    IEnumerator DesactivaDanio(){
        yield return new WaitForSeconds(0.4f);
        recibiendoDanio = false;
    }

    private void Movimiento()
    {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Si el jugador entra en el radio de detección, ¡ATACA!
        if (distanceToPlayer <= detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Voltea el sprite a la izquierda
            }
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Voltea el sprite a la derecha
            }
            movementX = direction.x; // Le decimos hacia dónde moverse
            enMovimiento = true; // Activamos la animación de caminar
        }
        // Si el jugador está lejos, ¡DETENTE!
        else
        {
            movementX = 0; // Frenamos al enemigo
            enMovimiento = false; // Apagamos la animación de caminar
        }
        if(!recibiendoDanio)
        rb2D.linearVelocity = new Vector2(movementX * speed, rb2D.linearVelocity.y);
    }

}
