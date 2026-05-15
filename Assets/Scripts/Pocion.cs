using UnityEngine;

public class Pocion : MonoBehaviour
{
    public int curacion = 1;
    
    // Se corrigió la "T" de Trigger y la "C" de Collider2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMove player = collision.GetComponent<playerMove>();
            player.CurarVida(curacion);
            Destroy(gameObject);
        }
    }
}
