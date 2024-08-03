using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private EmittedBeam _emittedBeam;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private void OnEnable()
    {
        _emittedBeam.BeamHitObject += TriggerExplosion;
    }

    private void OnDisable()
    {
        _emittedBeam.BeamHitObject -= TriggerExplosion;
    }

    public void TriggerExplosion(GameObject gameObject)
    {
        Vector3 explosionPosition = gameObject.transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _explosionRadius);

        foreach (Collider cube in colliders)
        {
            Rigidbody rigidbodyCube = cube.GetComponent<Rigidbody>();

            if (rigidbodyCube != null)
            {
                rigidbodyCube.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
            }
        }
    }
}
