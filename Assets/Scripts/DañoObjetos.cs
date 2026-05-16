using UnityEngine;

public class DañoObjetos : MonoBehaviour
{
    [Header("Configuración de Daño")]
    public int damageAmount = 1; // Bajado a 1 para que coincida con tus 3 vidas máximas

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Verificamos si lo que chocó es el Jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // 2. CAMBIO CRÍTICO: Buscamos el nuevo script 'movimientoRana' en lugar de playerMove
            movimientoRana scriptRana = collision.gameObject.GetComponent<movimientoRana>();

            if (scriptRana != null)
            {
                // 3. Creamos la posición de daño (solo nos interesa el eje X para el empuje)
                Vector2 direccionDanio = new Vector2(transform.position.x, 0);

                // 4. Le mandamos el daño y la posición a la rana para que reste vida y rebote
                scriptRana.RecibeDanio(direccionDanio, damageAmount);
            }
        }   
    }
}