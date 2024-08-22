using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeDivider : MonoBehaviour
{
    [SerializeField] private EmittedBeam _emittedBeam;
    [SerializeField] private GameObject _prefabCube;

    private int _minNumberCubs = 2;
    private int _maxNumberCubs = 7;
    private float _maxProcents = 100f;
    private List<Collider> _customCubesColliders;
    public event Action<List<Collider>, Vector3> CreatedCustomCube;

    private void OnEnable()
    {
        _emittedBeam.BeamHitCube += OnBeamHitCube;
    }

    private void OnDisable()
    {
        _emittedBeam.BeamHitCube -= OnBeamHitCube;
    }

    private void OnBeamHitCube(Vector3 positionCube, Vector3 localScaleCube, float probabilityCube)
    {
        float randomChance = UnityEngine.Random.Range(0, _maxProcents);

        if (randomChance < probabilityCube)
        {
            int numberCubs = UnityEngine.Random.Range(_minNumberCubs, _maxNumberCubs);

            for (int i = 0; i < numberCubs; i++)
            {
                GameObject prefabCube = Instantiate(_prefabCube);
                CustomCube customCube = prefabCube.GetComponent<CustomCube>();

                customCube.SetPositionDestroyedCube(positionCube);
                customCube.SetLocalScaleDestroyedCube(localScaleCube);
                customCube.SetProbabilityDestroyedCube(probabilityCube);
                customCube.SetTrgger();

                _customCubesColliders.Add(prefabCube.GetComponent<Collider>());
            }

            CreatedCustomCube?.Invoke(_customCubesColliders, positionCube);
            _customCubesColliders = null;
        }
        else
        {
            Debug.Log(this.name + " - Объект не делится. Вероятность " + probabilityCube + "%, случайное число: " + randomChance);
        }
    }
}