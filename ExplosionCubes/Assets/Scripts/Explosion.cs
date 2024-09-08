using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private CubeDivider _cubeDivider;
    [SerializeField, Min(0)] private float _force = 1;
    [SerializeField, Min(0)] private float _secondForce = 1;
    [SerializeField, Min(0)] private float _radius = 1;
    [SerializeField, Range(-1, 1)] private float _PositionY;

    private float _upwardsModifier = 1;

    private void OnValidate()
    {
        if (_cubeDivider == null)
        {
            Debug.Log($"{nameof(_cubeDivider)} = null");
        }
    }

    private void OnEnable()
    {
        _cubeDivider.CreatedCustomCube += OnCreatedCustomCube;
        _cubeDivider.DestroiedCube += OnDestroyCube;
    }

    private void OnDisable()
    {
        _cubeDivider.CreatedCustomCube -= OnCreatedCustomCube;
        _cubeDivider.DestroiedCube -= OnDestroyCube;
    }

    private void OnCreatedCustomCube(List<Collider> customCubesColliders, Vector3 positionDestroyedCustomCube)
    {

        Vector3 explosionPosition = positionDestroyedCustomCube + new Vector3(0, _PositionY, 0);

        foreach (Collider cubeCollider in customCubesColliders)
        {
            if (cubeCollider.GetComponent<Rigidbody>())
            {
                Rigidbody rigidbodyCubeCollider = cubeCollider.GetComponent<Rigidbody>();

                rigidbodyCubeCollider.AddExplosionForce(
                    _force,
                    explosionPosition,
                    _radius,
                    _upwardsModifier,
                    ForceMode.Impulse);
            }
        }
    }

    private void OnDestroyCube(Vector3 positionDestroyedCustomCube, float explosionRadius)
    {
        if (_secondForce > 0 && explosionRadius > 0)
        {
            Collider[] colliders = Physics.OverlapSphere(positionDestroyedCustomCube, explosionRadius);

            foreach (Collider cube in colliders)
            {
                Rigidbody rigidbodyCube = cube.GetComponent<Rigidbody>();

                if (rigidbodyCube != null)
                {
                    rigidbodyCube.AddExplosionForce(_secondForce, positionDestroyedCustomCube, explosionRadius);
                }
            }
        }
    }
}