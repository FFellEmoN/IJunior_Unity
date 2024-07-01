using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] float _delay = 0.5f;

    private int _leftButtonMouse = 0;
    private int _number;
    private bool _isLooping = true;
    private IEnumerator _myCorutine;

    private void Start()
    {
        _myCorutine = Countdown(_delay);

        StartCoroutine(_myCorutine);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftButtonMouse))
        {
            _isLooping = !_isLooping;
            enabled = _isLooping;
        }
    }

    private void OnDisable()
    {
        if (_myCorutine != null)
        {
            StopCoroutine(_myCorutine);
        }
    }

    private IEnumerator Countdown(float delay)
    {
        WaitForSecondsRealtime delayTime = new WaitForSecondsRealtime(delay);

        while (enabled)
        {
            Debug.Log(_number++);
            yield return delayTime;
        }
    }
}