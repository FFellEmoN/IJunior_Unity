using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _delay = 0.5f;

    public static event CounterCallback CountdownStarted;
    public delegate void CounterCallback(int number);
    private int _number;
    private int _leftButtonMouse = 0;
    private IEnumerator _corutineCountdown;

    private void Start()
    {
        CountdownStarted?.Invoke(_number);
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
            _number++;
            CountdownStarted?.Invoke(_number);
            yield return delayTime;
        }
    }
}