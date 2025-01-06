using UnityEngine;

public class MovementAlongSpecifiedPoints : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _targetPoints;

    private int _numberPlacePoint = 0;

    private void OnValidate()
    {
        if (_speed >= 0)
        {
            Debug.Log($"{nameof(_speed)} не инициализирован.");
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _targetPoints = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
            _targetPoints[i] = transform.GetChild(i);
    }
#endif


    private void Update()
    {
        Transform placePoint = _targetPoints[_numberPlacePoint];
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
        _numberPlacePoint = ++_numberPlacePoint % _targetPoints.Length;

        Vector3 vectorNewPoint = _targetPoints[_numberPlacePoint].position;
        transform.forward = vectorNewPoint - transform.position;
    }
}