using UnityEngine;
using UnityEngine.UI;
public class BarraVida : MonoBehaviour
{


    public Image rellenoBarraVida;
    private playerMove playerMove;
    private float vidaMaxima;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMove = GameObject.Find("player").GetComponent<playerMove>();
        vidaMaxima = playerMove.vida;
    }

    // Update is called once per frame
    void Update()
    {
        rellenoBarraVida.fillAmount = playerMove.vida / vidaMaxima;
    }
}
