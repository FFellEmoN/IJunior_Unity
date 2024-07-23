using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DividerCube : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Ray _ray;
    [SerializeField] private float _maxDistance = 100;
    [SerializeField] private CubeCreator _cubeCreator;

    private int _lefButtonMouse = 0;
    private int _minNumberCube = 2;
    private int _maxNumberCube = 7;

    private void Update()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        Debug.DrawRay(_ray.origin, _ray.direction * _maxDistance, Color.magenta);

        if (Input.GetMouseButtonDown(_lefButtonMouse))
        {
            if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
            {
                Destroy(hit.transform.gameObject);

                int randomValue = Random.Range(_minNumberCube, _maxNumberCube);

                for (int i = 0; i < randomValue; i++)
                {
                    _cubeCreator.Create(hit);
                }
            }
        }
    }
}
