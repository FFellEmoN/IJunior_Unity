using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _directionMovement;

    public event Action<Enemy> Fell;

    private void OnValidate()
    {
        if (_speed == 0)
        {
            Debug.Log($"{nameof(_speed)} не инициализирован.");
        }
    }

    private void Update()
    {
        Vector3 movement = _directionMovement.normalized * _speed * Time.deltaTime;

        transform.position += movement;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ZoneRelease>())
        {
            Fell?.Invoke(this);
        }
    }

    public void SetDirectionMovement(Vector3 directionMovement)
    {
        _directionMovement = directionMovement;
    }
}
