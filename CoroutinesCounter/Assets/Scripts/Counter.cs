using System.Collections;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private Renderer _renderer;

    public int Number { private set; get; }
    private int _leftButtonMouse = 0;
    private IEnumerator _myCorutine;

    private void Start()
    {
        _renderer.DisplayCountdown(Number);
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
            _renderer.DisplayCountdown(++Number);
            yield return delayTime;
        }
    }
}