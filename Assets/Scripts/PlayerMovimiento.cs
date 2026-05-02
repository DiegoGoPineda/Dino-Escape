using UnityEngine;

public class PlayerMovimiento : MonoBehaviour
{
    public float speed = 2f; // Variable para la velocidad de movimiento
    public float jumpSpeed = 5f; // Variable para la velocidad  de salto
    Rigidbody2D rb; // Variable para el componente Rigidbody2D
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D al inicio

    }


    void FixedUpdate()
    {   
        // teclas dereca
        if (Input.GetKey("d") || Input.GetKey("right")) 
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y); // Mover hacia la derecha
        }
        // teclas izquierda
        else if (Input.GetKey("a") || Input.GetKey("left")) 
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y); // Mover hacia la  izquierda
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // quieto
        }
        // salto
        if (Input.GetKey("space") && CheckPiso.isGrounded) 
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed); // Saltar
        }
    }
}
