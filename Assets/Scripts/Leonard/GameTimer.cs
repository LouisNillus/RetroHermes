using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private float timer;
    [SerializeField] Text timerText;

    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = (Mathf.Round(timer * 10f) / 10f).ToString();
    }
}