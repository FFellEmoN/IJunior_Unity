using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private CubeDivider _cubeDivider;
    [SerializeField, Min(0)] private float _Force = 1;
    [SerializeField, Min(0)] private float _Radius = 1;
    [SerializeField, Range(-1, 1)] private float _PositionY;

    private float _upwardsModifier = 1;

    private void OnEnable()
    {
        if (_cubeDivider != null)
        {
            _cubeDivider.CreatedCustomCube += OnCreatedCustomCube;
        }
        else
        {
            Debug.Log($"{nameof(_cubeDivider)} = null");
        }
    }

    private void OnDisable()
    {
        if (_cubeDivider != null)
        {
            _cubeDivider.CreatedCustomCube -= OnCreatedCustomCube;
        }
        else
        {
            Debug.Log($"{nameof(_cubeDivider)} = null");
        }
    }

    private void OnCreatedCustomCube(List<Collider> customCubesColliders, Vector3 positionDestroyedCustomCube)
    {

        Vector3 explosionPosition = positionDestroyedCustomCube + new Vector3(0, _PositionY, 0);

        foreach (Collider cubeCollider in customCubesColliders)
        {
            Rigidbody rigidbodyCubeCollider = cubeCollider.GetComponent<Rigidbody>();

            if (rigidbodyCubeCollider != null)
            {
                rigidbodyCubeCollider.AddExplosionForce(_Force,
                    explosionPosition,
                    _Radius,
                    _upwardsModifier,
                    ForceMode.Impulse);
            }
        }
    }
}