using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaSeleccion : MonoBehaviour
{
    public void Kit() {
        SceneManager.LoadScene("Kit");
    }

     public void Historia() {
        SceneManager.LoadScene("Historia1");
    }

     public void Volver(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
