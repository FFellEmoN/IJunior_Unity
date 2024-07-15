using TMPro;
using UnityEngine;

public class Renderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        Counter.CountdownStarted += DisplayCountdown;
    }

    public void DisplayCountdown(int number)
    {
        _text.text = number.ToString("");
    }
}
