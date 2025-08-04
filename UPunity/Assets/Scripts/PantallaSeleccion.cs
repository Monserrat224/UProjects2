using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaSeleccion : MonoBehaviour
{
    public void Characters() {
        SceneManager.LoadScene("Personajes");
    }

     public void Historia() {
        SceneManager.LoadScene("Historia");
    }

     public void Volver(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
