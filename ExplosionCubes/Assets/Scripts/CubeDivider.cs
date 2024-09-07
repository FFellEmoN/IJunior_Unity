using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeDivider : MonoBehaviour
{
    [SerializeField] private EmittedBeam _emittedBeam;
    [SerializeField] private GameObject _prefabCube;
    [SerializeField] private float _explosionForce;

    private int _minNumberCubs = 2;
    private int _maxNumberCubs = 6;
    private float _maxProcents = 100f;
    private List<Collider> _customCubesColliders;

    public event Action<List<Collider>, Vector3> CreatedCustomCube;
    public event Action<Vector3, float> DestroiedCube;

    private void Start()
    {
        _customCubesColliders = new List<Collider>();
    }

    private void OnValidate()
    {
        if (_prefabCube == null)
        {
            Debug.LogError($"{nameof(_prefabCube)} не установлен в {nameof(CubeDivider)} в {gameObject.name}");
        }
        else
        {
            if (_prefabCube.GetComponent<CustomCube>() == null)
            {
                Debug.LogError($"{_prefabCube.name} должен содержать компонент {nameof(CustomCube)}.");
            }

            if (_prefabCube.GetComponent<Collider>() == null)
            {
                Debug.LogError($"{_prefabCube.name} должен содержать компонент Collider.");
            }
        }

        if (_emittedBeam == null)
        {
            Debug.Log($"{nameof(_emittedBeam)} = null");
        }
    }

    private void OnEnable()
    {
        _emittedBeam.BeamHitCube += OnBeamHitCube;
    }

    private void OnDisable()
    {
        _emittedBeam.BeamHitCube -= OnBeamHitCube;
    }

    private void OnBeamHitCube(Vector3 positionCube, Vector3 localScaleCube, float probabilityCube, float explosionRadius)
    {
        float randomChance = UnityEngine.Random.Range(0, _maxProcents);

        if (randomChance < probabilityCube)
        {
            int numberCubs = UnityEngine.Random.Range(_minNumberCubs, _maxNumberCubs + 1);

            for (int i = 0; i < numberCubs; i++)
            {
                GameObject prefabCube = Instantiate(_prefabCube, positionCube, Quaternion.identity);

                CustomCube customCube = prefabCube.GetComponent<CustomCube>();

                customCube.Init(localScaleCube, positionCube, probabilityCube, explosionRadius);

                _customCubesColliders.Add(prefabCube.GetComponent<Collider>());
            }

            CreatedCustomCube?.Invoke(_customCubesColliders, positionCube);
            _customCubesColliders = new List<Collider>();
        }
        else
        {
            Debug.Log(this.name + " - Объект не делится. Вероятность " + probabilityCube + "%, случайное число: " + randomChance);

            DestroiedCube?.Invoke(positionCube, explosionRadius);
        }
    }
}