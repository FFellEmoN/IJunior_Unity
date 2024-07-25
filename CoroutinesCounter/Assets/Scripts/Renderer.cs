using TMPro;
using UnityEngine;

public class Renderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Counter _counter;

    private void OnEnable()
    {
        _counter.Changed += DisplayCountdown;
    }

    private void OnDisable()
    {
        _counter.Changed -= DisplayCountdown;
    }

    public void DisplayCountdown(int number)
    {
        _text.text = number.ToString("");
    }
}
