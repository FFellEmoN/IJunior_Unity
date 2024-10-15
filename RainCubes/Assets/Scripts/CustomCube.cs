using CubesRain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCube : MonoBehaviour
{
    [SerializeField] private GameObject _mainPlatform;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private List<Material> _colors;

    private bool _wasContactPlane = false;
    private string _nameStandardMatirial = "Standard";
    private Coroutine _coroutine;
    private SpawnerCubes _spawner;

    public void SetWasContactPlane()
    {
        _wasContactPlane = false;
    }

    public void SetStandardColor()
    {
        _meshRenderer.material = new Material(Shader.Find(_nameStandardMatirial));
    }

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

        if (_mainPlatform == null)
        {
            Debug.Log($"{nameof(_mainPlatform)} = null");
        }
    }

    private void Awake()
    {
        _spawner = FindObjectOfType<SpawnerCubes>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        int minLiveTime = 2;
        int maxLiveTime = 5;

        if (collision.gameObject.tag == _mainPlatform.tag)
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

    private void SetColor()
    {
        int randomValue = UnityEngine.Random.Range(0, _colors.Count);

        _meshRenderer.material = _colors[randomValue];
    }

    private IEnumerator Countdown(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        _spawner.ReleaseCube(gameObject);
    }
}
