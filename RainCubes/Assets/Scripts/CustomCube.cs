using CubesRain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCube : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ChangerColor _color;
    [SerializeField] private Rigidbody _rigidbody;

    private bool _wasContactPlane = false;
    private Coroutine _coroutine;

    public event Action<CustomCube> TimeRelese;

    private void OnValidate()
    {
        if (_meshRenderer == null)
        {
            Debug.Log($"{nameof(_meshRenderer)} = null");
        }

        if (_color == null)
        {
            Debug.Log($"{nameof(_color)} is eampty.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        int minLiveTime = 2;
        int maxLiveTime = 5;

        if (collision.gameObject.GetComponent<Platform>())
        {
            if (_wasContactPlane == false)
            {
                _wasContactPlane = true;

                SetColor();

                int randomTimeLive = UnityEngine.Random.Range(minLiveTime, maxLiveTime + 1);

                _coroutine = StartCoroutine(Countdown(randomTimeLive));
            }
        }
    }

    public void Init(bool isActive)
    {
        if (isActive == true)
        {
            _rigidbody.velocity = Vector3.zero;
        }
        else
        {
            SetWasContactPlane();
        }
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private void SetWasContactPlane()
    {
        _wasContactPlane = false;
        SetColor();
    }

    private void SetColor()
    {
        if (_wasContactPlane == true)
            _color.SetRandom();
        else
            _color.SetDefault();
    }

    private IEnumerator Countdown(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        TimeRelese?.Invoke(this);
    }
}
