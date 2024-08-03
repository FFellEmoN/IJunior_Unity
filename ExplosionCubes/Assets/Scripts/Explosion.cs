using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private EmittedBeam _emittedBeam;
    private float _explosionForce = 1000f;

    private float _explosionRadius = 5f;
    private Coroutine _coroutine;

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
