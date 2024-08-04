using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private EmittedBeam _emittedBeam;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private void OnEnable()
    {
        _emittedBeam.BeamHitObject += OnBeamHitObject;
    }

    private void OnDisable()
    {
        _emittedBeam.BeamHitObject -= OnBeamHitObject;
    }

    private void OnBeamHitObject(GameObject destroyedCube)
    {
        if (_explosionForce > 0 && _explosionRadius > 0) {
            Vector3 explosionPosition = destroyedCube.transform.position;

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
        else
        {
            Debug.Log("Неверно введины данные радиуса или силы взрыва.");
        }
    }
}
