using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform[] _waipoints;
    [SerializeField] private float _speed;

    private int _currentWaypoint = 0;

    private void Update()
    {
        if (transform.position == _waipoints[_currentWaypoint].position)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waipoints.Length;
        }

        transform.position = Vector3.MoveTowards(transform.position, _waipoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }
}
