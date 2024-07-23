using TMPro;
using UnityEngine;

public class Renderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Counter _counter;

    private void Update()
    {
        DisplayCountdown();
    }

    public void DisplayCountdown()
    {
        _text.text = _counter.Number.ToString("");
    }
}
