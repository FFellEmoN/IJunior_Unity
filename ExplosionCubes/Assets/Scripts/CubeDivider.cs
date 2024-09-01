using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeDivider : MonoBehaviour
{
    [SerializeField] private EmittedBeam _emittedBeam;
    [SerializeField] private GameObject _prefabCube;
    [SerializeField] private float _explosionForce;

    private int _minNumberCubs = 2;
    private int _maxNumberCubs = 7;
    private float _maxProcents = 100f;
    private List<Collider> _customCubesColliders;
    public event Action<List<Collider>, Vector3> CreatedCustomCube;

    private void Start()
    {
        _customCubesColliders = new List<Collider>();
    }

    private void OnEnable()
    {
        if (_emittedBeam != null)
        {
            _emittedBeam.BeamHitCube += OnBeamHitCube;
        }
        else
        {
            Debug.Log($"{nameof(_emittedBeam)} = null");
        }
    }

    private void OnDisable()
    {
        if (_emittedBeam != null)
        {
            _emittedBeam.BeamHitCube -= OnBeamHitCube;
        }
        else
        {
            Debug.Log($"{nameof(_emittedBeam)} = null");
        }
    }

    private void OnBeamHitCube(Vector3 positionCube, Vector3 localScaleCube, float probabilityCube, float explosionRadius)
    {
        float randomChance = UnityEngine.Random.Range(0, _maxProcents);

        if (randomChance < probabilityCube)
        {
            int numberCubs = UnityEngine.Random.Range(_minNumberCubs, _maxNumberCubs);

            for (int i = 0; i < numberCubs; i++)
            {
                if (_prefabCube != null)
                {
                    GameObject prefabCube = Instantiate(_prefabCube, positionCube, Quaternion.identity);

                    if (prefabCube.GetComponent<CustomCube>())
                    {
                        CustomCube customCube = prefabCube.GetComponent<CustomCube>();

                        customCube.SetPosition(positionCube);
                        customCube.SetLocalScale(localScaleCube);
                        customCube.SetProbability(probabilityCube);
                        customCube.SetExplosionRadius(explosionRadius);

                        if (prefabCube.GetComponent<Collider>())
                        {
                            _customCubesColliders.Add(prefabCube.GetComponent<Collider>());
                        }
                        else
                        {
                            Debug.Log($"{prefabCube.name} не имеет компонента Collider.");
                        }
                    }
                }
                else
                {
                    Debug.Log($"{nameof(_prefabCube)} = null");
                }
            }

            CreatedCustomCube?.Invoke(_customCubesColliders, positionCube);
            _customCubesColliders = new List<Collider>();
        }
        else
        {
            Debug.Log(this.name + " - Объект не делится. Вероятность " + probabilityCube + "%, случайное число: " + randomChance);

            if (_explosionForce > 0 && explosionRadius > 0)
            {
                Collider[] colliders = Physics.OverlapSphere(positionCube, explosionRadius);

                foreach (Collider cube in colliders)
                {
                    Rigidbody rigidbodyCube = cube.GetComponent<Rigidbody>();

                    if (rigidbodyCube != null)
                    {
                        rigidbodyCube.AddExplosionForce(_explosionForce, positionCube, explosionRadius);
                    }
                }
            }
        }
    }
}