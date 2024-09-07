using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCube : MonoBehaviour
{
    [SerializeField] private List<Material> _colorsCubes;

    private bool _wasContactPlane = false;
    private Coroutine _coroutine;
    private int _minLiveTime = 2;
    private int _maxLiveTime = 6;

    public void SetWasContactPlane()
    {
        _wasContactPlane = true;
    }

    public void SetColor(Material material)
    {
        if (material != null)
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = material;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_wasContactPlane == false)
        {
            _wasContactPlane = true;

            SetColor();

            int randomValueDelay = Random.Range(_minLiveTime, _maxLiveTime);
            _coroutine = StartCoroutine(Countdown(randomValueDelay));
        }
    }

    private void SetColor()
    {
        if (_colorsCubes.Count > 0)
        {
            int randomValue = Random.Range(0, _colorsCubes.Count);

            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = _colorsCubes[randomValue];
        }
        else
        {
            Debug.Log($"{nameof(_colorsCubes)} is eampty.");
        }
    }

    private IEnumerator Countdown(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Destroy(gameObject);
    }
}
