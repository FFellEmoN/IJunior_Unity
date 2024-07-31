using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EmittedBeam : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxDistance = 100;

    public event Action<GameObject> OnCollisionChange;
    private Ray _ray;
    private int _lefButtonMouse = 0;

    private void Update()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        Debug.DrawRay(_ray.origin, _ray.direction * _maxDistance, Color.magenta);

        if (Input.GetMouseButtonDown(_lefButtonMouse))
        {
            if (Physics.Raycast(_ray, out hit, Mathf.Infinity) && hit.transform.gameObject.CompareTag("Cube"))
            {
                OnCollisionChange?.Invoke(hit.transform.gameObject);
            }
        }
    }
}