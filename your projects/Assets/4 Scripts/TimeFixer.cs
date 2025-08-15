using UnityEngine;

public class TimeFixer : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
        Debug.Log("Tiempo restaurado a 1");
    }
}
