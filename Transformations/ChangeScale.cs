using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    [SerializeField] private float _scaleSpeed = 1f;
    [SerializeField] private float _minScale = 0.5f;
    [SerializeField] private float _maxScale = 2f;

    private Vector3 _targetScale;
    private bool _isIncreases = true;

    private void Start()
    {
        _targetScale = transform.localScale;
    }

    private void Update()
    {
        var scaleFactor = 1 + _scaleSpeed * Time.deltaTime;

        if (_isIncreases && _targetScale.x >= _maxScale)
        {
            _isIncreases = false;
        }

        if (_isIncreases == false && _targetScale.x <= _minScale)
        {
            _isIncreases = true;
        }

        _targetScale *= _isIncreases ? scaleFactor : 1f / scaleFactor;

        _targetScale.x = Mathf.Clamp(_targetScale.x, _minScale, _maxScale);
        _targetScale.y = Mathf.Clamp(_targetScale.y, _minScale, _maxScale);
        _targetScale.z = Mathf.Clamp(_targetScale.z, _minScale, _maxScale);

        transform.localScale = _targetScale;
    }
}