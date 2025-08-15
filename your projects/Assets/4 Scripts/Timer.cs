using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI timerText;
    [SerializeField] public float remainingTime;

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            //GameOver();
            timerText.color = Color.red;
        }

        // Solo calculamos los segundos
        int seconds = Mathf.FloorToInt(remainingTime);
        timerText.text = string.Format("{0:00}", seconds);
    }
}

