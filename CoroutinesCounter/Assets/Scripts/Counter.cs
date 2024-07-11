using System.Collections;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private Renderer _renderer;

    private int _number;
    private int _leftButtonMouse = 0;
    private IEnumerator _corutineCountdown;

    private void Start()
    {
        _renderer.DisplayCountdown(_number);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftButtonMouse))
        {
            if(_corutineCountdown != null)
            {
                StopCoroutine(_corutineCountdown);

                _corutineCountdown = null;
            }
            else
            {
                _corutineCountdown = Countdown(_delay);

                StartCoroutine(_corutineCountdown);
            }
        }
    }

    private IEnumerator Countdown(float delay)
    {
        WaitForSecondsRealtime delayTime = new WaitForSecondsRealtime(delay);

        while (enabled)
        {
            _renderer.DisplayCountdown(++_number);
            yield return delayTime;
        }
    }
}