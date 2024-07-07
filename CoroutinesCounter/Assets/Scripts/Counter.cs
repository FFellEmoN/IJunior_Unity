using System.Collections;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private TextMeshProUGUI _text;

    private int _leftButtonMouse = 0;
    private int _number;
    private IEnumerator _myCorutine;

    private void Start()
    {
        _text.text = _number.ToString("");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftButtonMouse))
        {
            if(_myCorutine != null)
            {
                StopCoroutine(_myCorutine);

                _myCorutine = null;
            }
            else
            {
                _myCorutine = Countdown(_delay);

                StartCoroutine(_myCorutine);
            }
        }
    }

    private IEnumerator Countdown(float delay)
    {
        WaitForSecondsRealtime delayTime = new WaitForSecondsRealtime(delay);

        while (enabled)
        {
            _number++;

            DisplayCountdown(_number);
            
            yield return delayTime;
        }
    }

    private void DisplayCountdown(int number)
    {
        _text.text = number.ToString("");
    }
}