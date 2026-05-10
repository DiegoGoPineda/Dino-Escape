using UnityEngine;

public class DañoObjetos : MonoBehaviour
{
    public int damageAmount = 10; // Cantidad de daño que el objeto inflige

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject); // Destruye el objeto del jugador al colisionar
        }   
    }
}
