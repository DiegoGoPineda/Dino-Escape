using UnityEngine;

public class CheckPiso : MonoBehaviour
{
   public static bool isGrounded; // Variable para verificar si el jugador está en el suelo

   private void OnTriggerEnter2D(Collider2D collision)
   {    
           isGrounded = true; // El jugador está en el suelo
    
   }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
              isGrounded = false; // El jugador no está en el suelo
    }
}
