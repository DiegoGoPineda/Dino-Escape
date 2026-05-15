using UnityEngine;

public class ZonaMuerte : MonoBehaviour
{
   private void OnTriggerEnter2D( Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Se corrigió la D mayúscula
            Debug.Log("Jugador ha entrado en la zona de muerte");
            
            // Se corrigió "RecibirDanio" por el nombre real de tu función: "RecibeDanio"
            collision.GetComponent<playerMove>().RecibeDanio(Vector2.zero, 99); 
        }
    }
}
