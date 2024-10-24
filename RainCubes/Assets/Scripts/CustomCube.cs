using CubesRain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCube : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private List<Material> _colors;

    private bool _wasContactPlane = false;
    private Coroutine _coroutine;

    public event Action<CustomCube> TimeRelese;

    private void OnValidate()
    {
        if (_meshRenderer == null)
        {
            Debug.Log($"{nameof(_meshRenderer)} = null");
        }

        if (_colors.Count == 0)
        {
            Debug.Log($"{nameof(_colors)} is eampty.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        int minLiveTime = 2;
        int maxLiveTime = 5;

        if (collision.gameObject.GetComponent<EmptyScriptForPlatform>())
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

    public void SetWasContactPlane()
    {
        _wasContactPlane = false;
    }

    public void SetStandardColor(Material standardMatirial)
    {
        _meshRenderer.material = new Material(standardMatirial);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private void SetColor()
    {
        int randomValue = UnityEngine.Random.Range(0, _colors.Count);

        _meshRenderer.material = _colors[randomValue];
    }

    private IEnumerator Countdown(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        TimeRelese?.Invoke(this);
    }
}
