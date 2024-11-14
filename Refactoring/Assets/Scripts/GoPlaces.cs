using UnityEngine;

public class GoPlaces : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _placesPoint;

    private Transform[] _arrayPlaces;
    private int _numberPlacePoint = 0;

    private void OnValidate()
    {
        if (_speed == 0)
        {
            Debug.Log($"{nameof(_speed)} не инициализирован.");
        }

        if (_placesPoint == null)
        {
            Debug.Log($"{nameof(_placesPoint)} не инициализирован .");
        }

        if (_placesPoint.childCount == 0)
        {
            Debug.Log($"{nameof(_placesPoint)} у него нет дочерних объектов.");
        }
    }

    void Start()
    {
        _arrayPlaces = new Transform[_placesPoint.childCount];

        for (int i = 0; i < _placesPoint.childCount; i++)
            _arrayPlaces[i] = _placesPoint.GetChild(i);
    }

    private void Update()
    {
        Transform placePoint = _arrayPlaces[_numberPlacePoint];
        transform.position = Vector3.MoveTowards(
            transform.position,
            placePoint.position,
            _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, placePoint.position) < 0.01f)
        {
            UpdateNextPoint();
        }
    }

    private void UpdateNextPoint()
    {
        _numberPlacePoint++;

        if (_numberPlacePoint >= _arrayPlaces.Length)
            _numberPlacePoint = 0;

        Vector3 vectorNewPoint = _arrayPlaces[_numberPlacePoint].position;
        transform.forward = vectorNewPoint - transform.position;
    }
}