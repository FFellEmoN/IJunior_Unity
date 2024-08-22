using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private CubeDivider _cubeDivider;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private void OnEnable()
    {
        _cubeDivider.CreatedCustomCube += OnCreatedCustomCube;
    }

    private void OnDisable()
    {
        _cubeDivider.CreatedCustomCube -= OnCreatedCustomCube;
    }

    private void OnCreatedCustomCube(List<Collider> customCubesColliders, Vector3 explosionPosition)
    {
        if (_explosionForce > 0 && _explosionRadius > 0)
        {
            foreach (Collider customCubeCollider in customCubesColliders)
            {
                Rigidbody rigidbodyCube = customCubeCollider.GetComponent<Rigidbody>();

                if (rigidbodyCube != null)
                {
                    rigidbodyCube.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
                }
            }
        }
        else
        {
            Debug.Log("Неверно введины данные радиуса или силы взрыва.");
        }
    }
}
