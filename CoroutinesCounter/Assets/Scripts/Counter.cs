using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] float _delay = 0.5f;

    private int _leftButtonMouse = 0;
    private int _value = 1;
    private int _number;
    private bool _isCoroutineRunning = false;
    private IEnumerator _myCorutine;
    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new WaitForSeconds(_delay);
        _myCorutine = Countdown();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftButtonMouse))
        {
            _value++;
        }

        if (_value % 2 == 0)
        {
            if (_isCoroutineRunning == false)
            {
                StartCoroutine(_myCorutine);

                _isCoroutineRunning = true;
            }
        }
        else
        {
            StopCoroutine(_myCorutine);

            _isCoroutineRunning = false;
        }
    }

    private IEnumerator Countdown()
    {
        while (true)
        {
            yield return _wait;
            Debug.Log(_number++);
        }
    }
}