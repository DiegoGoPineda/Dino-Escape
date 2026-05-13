using UnityEngine;

public class CamaraControl : MonoBehaviour
{
   public Transform objetivo; 
   public float velocidadCamara = 0.025f;// La velocidad a la que la cámara seguirá al objetivo
   public Vector3 desplazamiento; // El desplazamiento de la cámara con respecto al objetivo

   private void LateUpdate()
   {
       // Calcula la posición deseada de la cámara sumando el desplazamiento al objetivo
       Vector3 posicionDeseada = objetivo.position + desplazamiento;
       
       Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCamara);
       transform.position = posicionSuavizada; // Actualiza la posición de la cámara
   }
}
