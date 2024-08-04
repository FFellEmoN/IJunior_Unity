using System;
using UnityEngine;

public class EmittedBeam : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxDistance = 100;

    public event Action<GameObject> BeamHitObject;
    private Ray _ray;
    private int _lefButtonMouse = 0;
    private string _tagCube = "Cube";

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
                    if (hit.transform.gameObject.CompareTag(_tagCube))
                    {
                        BeamHitObject?.Invoke(hit.transform.gameObject);
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