using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _delay = 0.5f;

    public event Action<int> Changed;
    private Coroutine _coroutine;
    private int _leftButtonMouse = 0;
    private int _number;

    private void Start()
    {
        Changed?.Invoke(_number);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftButtonMouse))
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);

                _coroutine = null;
            }
            else
            {
                _coroutine = StartCoroutine(Countdown(_delay));
            }
        }
    }

    private IEnumerator Countdown(float delay)
    {
        WaitForSecondsRealtime delayTime = new WaitForSecondsRealtime(delay);

        while (enabled)
        {
            _number++;
            Changed?.Invoke(_number);
            yield return delayTime;
        }
    }
}