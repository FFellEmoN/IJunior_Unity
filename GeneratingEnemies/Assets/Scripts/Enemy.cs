using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rigidbody;

    private MovementCube _directionMovement;

    public event Action<Enemy> AchievedTarget;

    private void OnValidate()
    {
        if (_speed == 0 && isActiveAndEnabled == true)
        {
            Debug.Log($"{nameof(_speed)} не инициализирован.");
        }
    }

    private void Update()
    {
        Vector3 direction = (_directionMovement.gameObject.transform.position - transform.position).normalized;

        _rigidbody.MovePosition(transform.position + direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MovementCube>() == _directionMovement)
        {
            AchievedTarget?.Invoke(this);
        }
    }

    public void SetTarget(MovementCube targetMovement)
    {
        _directionMovement = targetMovement;
    }
}
