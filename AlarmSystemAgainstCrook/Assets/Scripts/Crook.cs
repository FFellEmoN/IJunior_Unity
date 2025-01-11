using UnityEngine;

public class Crook : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private TargetPoints _targetPoints;

    private int _indexPlacePoint = 0;

    private void OnValidate()
    {
        if (_speed <= 0)
        {
            Debug.Log($"{nameof(_speed)} некоректное значение скорости.");
        }

        if (_targetPoints == null)
        {
            Debug.Log($"{nameof(_targetPoints)} не инициализирован.");
        }
    }

    private void Update()
    {
        Transform placePoint = _targetPoints.Get(_indexPlacePoint);
        transform.position = Vector3.MoveTowards(
            transform.position,
            placePoint.position,
            _speed * Time.deltaTime);
        Vector3 differenceVector = transform.position - placePoint.position;
        float distanceСomparison = 0.0001f;

        if (differenceVector.sqrMagnitude < distanceСomparison)
        {
            UpdateNextPoint();
        }
    }

    private void UpdateNextPoint()
    {
        _indexPlacePoint = ++_indexPlacePoint % _targetPoints.LengthArray;

        Vector3 vectorNewPoint = _targetPoints.Get(_indexPlacePoint).position;
        transform.forward = vectorNewPoint - transform.position;
    }
}
