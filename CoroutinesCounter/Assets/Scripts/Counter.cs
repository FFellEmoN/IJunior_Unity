using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _delay = 0.5f;

    public event Action CountdownStarted;
    public int Number { private set; get; }
    private int _leftButtonMouse = 0;
    private IEnumerator _corutineCountdown;

    private void Start()
    {
        CountdownStarted?.Invoke();
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
            Number++;
            CountdownStarted?.Invoke();
            yield return delayTime;
        }
    }
}