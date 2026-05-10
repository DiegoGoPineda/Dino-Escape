using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlFrutas : MonoBehaviour
{

    private void Update()
    {
        AllFrutas();
    }
    public void AllFrutas()
    {
        if(transform.childCount == 0)
        {
            Debug.Log("Todas las frutas han sido recolectadas");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
