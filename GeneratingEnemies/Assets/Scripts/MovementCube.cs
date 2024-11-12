using UnityEngine;

public class MovementCube : MonoBehaviour
{
    [SerializeField] private Transform[] _waipoints;
    [SerializeField] private float _speed;

    private int _currentWaypoint = 0;

    private void OnValidate()
    {
        if (this.isActiveAndEnabled)
        {
            if (_waipoints.Length == 0)
            {
                Debug.Log($"{nameof(_waipoints)}, не инициализирован у объекта {this.name}.");
            }
        }

        if (_speed == 0)
        {
            Debug.Log($"{nameof(_speed)}, не инициализирован.");
        }
    }

    private void Update()
    {
        if (transform.position == _waipoints[_currentWaypoint].position)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waipoints.Length;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            _waipoints[_currentWaypoint].position,
            _speed * Time.deltaTime);
    }
}