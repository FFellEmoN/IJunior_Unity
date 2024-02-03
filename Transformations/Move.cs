using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _positionEndRoad;
    private Vector3 _movementPosition;
    private float _startPositionZ;

    private void Start()
    {
        _startPositionZ = transform.position.z;
    }

    private void Update()
    {
        var position = transform.position;

        if (transform.position.z > _positionEndRoad.position.z)
        {
            position.z = _positionEndRoad.position.z - 0.1f;
            _speed *= -1;
        }
        else if(transform.position.z < _startPositionZ)
        {
            position.z = _startPositionZ + 0.1f;
            _speed *= -1;
        }

        _movementPosition.z = _speed * Time.deltaTime;
        transform.Translate(_movementPosition);
    }
}
