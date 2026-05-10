using UnityEngine;

public class ControllCamara : MonoBehaviour
{
    public Transform objetivo; // objetivo de la camara
    public float velocidadCamara = 0.025f; // suavizado de movimiento de la camara
    public Vector3 distanciaCamara; // distancia entre la camara y el objetivo
    
    private void LateUpdate()
    {
        Vector3 posicionDeseada = objetivo.position + distanciaCamara; // posicion deseada de la camara
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCamara); // suavizado de movimiento
        transform.position = posicionSuavizada; // actualizar la posicion de la camara
    }
}
