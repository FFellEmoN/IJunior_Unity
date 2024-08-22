using System;
using UnityEngine;

public class EmittedBeam : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxDistance = 100;

    private Ray _ray;
    private int _lefButtonMouse = 0;
    public event Action<Vector3, Vector3, float> BeamHitCube;

    private void Update()
    {
        if (_camera != null  && _maxDistance > 0)
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            Debug.DrawRay(_ray.origin, _ray.direction * _maxDistance, Color.magenta);

            if (Input.GetMouseButtonDown(_lefButtonMouse))
            {
                if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
                {
                    GameObject affectedGameObject = hit.transform.gameObject;

                    if (affectedGameObject.GetComponent<CustomCube>())
                    {
                        BeamHitCube?.Invoke(
                            affectedGameObject.transform.position, 
                            affectedGameObject.transform.localScale,
                            affectedGameObject.GetComponent<ProbabilityDivision>().GetValue());

                        Destroy(affectedGameObject);
                    }
                }
            }
        }
        else
        {
            Debug.Log("Камера не инициализированна или неверно задана дистанция камеры.");
        }
    }
}