using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeDivider : MonoBehaviour
{
    [SerializeField] private EmittedBeam _emittedBeam;
    [SerializeField] private CustomCube _prefab;
    [SerializeField] private float _explosionForce;

    private int _minNumberCubs = 2;
    private int _maxNumberCubs = 6;
    private float _maxProcents = 100f;

    public event Action<List<Collider>, Vector3> CreatedCustomCube;
    public event Action<Vector3, float> DestroiedCube;

    private void OnValidate()
    {
        if (_prefab == null)
        {
            Debug.LogError($"{nameof(_prefab)} не установлен в {nameof(CubeDivider)} в {gameObject.name}");
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
        List<Collider> customCubesColliders = new List<Collider>();

        if (randomChance < probabilityCube)
        {
            int numberCubs = UnityEngine.Random.Range(_minNumberCubs, _maxNumberCubs + 1);

            for (int i = 0; i < numberCubs; i++)
            {
                CustomCube customCube = Instantiate(_prefab, positionCube, Quaternion.identity);

                customCube.Init(localScaleCube, positionCube, probabilityCube, explosionRadius);

                customCubesColliders.Add(customCube.GetCollider());
            }

            CreatedCustomCube?.Invoke(customCubesColliders, positionCube);
        }
        else
        {
            Debug.Log(this.name + " - Объект не делится. Вероятность " + probabilityCube + "%, случайное число: " + randomChance);

            DestroiedCube?.Invoke(positionCube, explosionRadius);
        }
    }
}