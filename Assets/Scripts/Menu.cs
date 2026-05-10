using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject opciones;
    public void jugar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir(){
    // Esto te confirmará en el editor que el botón sí funciona
    Debug.Log("Saliendo de Dino-Escape..."); 
    
    // Esto cerrará el juego real cuando ya esté compilado
    Application.Quit(); 
    }

    public void Opciones(){
    menu.SetActive(false);
    opciones.SetActive(true);
    }

     public void Atras(){
    menu.SetActive(true);
    opciones.SetActive(false);
    }
}
