using System;
using UnityEngine;

public class EmittedBeam : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField, Min(0)] private float _maxDistance = 100;

    private Ray _ray;
    private int _rayEmitter = 0;
    public event Action<Vector3, Vector3, float> BeamHitCube;

    private void Update()
    {
        if (_camera != null)
        {
            RaycastHit[] hits;

            _ray = _camera.ScreenPointToRay(Input.mousePosition); 
            Debug.DrawRay(_ray.origin, _ray.direction * _maxDistance, Color.magenta);

            if (Input.GetMouseButtonDown(_rayEmitter))
            {
                hits = Physics.RaycastAll(_ray, _maxDistance);
                Array.Sort(hits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

                if (hits.Length > 0)
                {
                    foreach (RaycastHit hit in hits)
                    {
                        GameObject receivedGameObject = hit.transform.gameObject;

                        if (receivedGameObject.GetComponent<CustomCube>() && 
                            receivedGameObject.GetComponent<ProbabilityDivision>())
                        {
                            BeamHitCube?.Invoke(
                                receivedGameObject.transform.position,
                                receivedGameObject.transform.localScale,
                                receivedGameObject.GetComponent<ProbabilityDivision>().GetValue());

                            Destroy(receivedGameObject);
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log($"{nameof(_camera)} = null");
        }
    }
}