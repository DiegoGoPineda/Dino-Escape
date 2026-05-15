using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button menuButton;

    private bool gameOverActivo = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(gameOverPanel != null)
            gameOverPanel.SetActive(false);
        if(restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
        if(menuButton != null)
            menuButton.onClick.AddListener(ReturnToMenu);
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        // Si la pantalla de Game Over está activa, esperamos a que el jugador presione una tecla
        if (gameOverActivo)
        {
            // Solo reiniciamos SI el jugador presiona la tecla 'R'
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
            
            // Solo vamos al menú SI el jugador presiona la tecla 'M'
            if (Input.GetKeyDown(KeyCode.M))
            {
                ReturnToMenu();
            }
        }

        // Este lo dejamos aquí afuera por si quieres usar Escape para salir en cualquier momento
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            ReturnToMenu();
        }
    }

    public void GameOver()
    {
        if(gameOverActivo) return;
        gameOverActivo = true;
        if(gameOverPanel != null){
            gameOverPanel.SetActive(true);
        }
        if(gameOverText != null){
            gameOverText.text = "GameOver";

        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu_Inicio");
    }
}
