using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;

    private void OnValidate()
    {
        if (_direction == Vector3.zero)
        {
            Debug.Log($"{nameof(_direction)} не инициализирован.");
        }
    }

    public Vector3 GetDirection() { return _direction; }
}
