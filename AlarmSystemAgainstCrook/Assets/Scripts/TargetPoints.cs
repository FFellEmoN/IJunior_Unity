using UnityEngine;

public class TargetPoints : MonoBehaviour
{
    [SerializeField] private Transform[] _targetPoints;

    public int LengthArray { get; private set; }

    private void Start()
    {
        LengthArray = _targetPoints.Length;
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

    public Transform Get(int index) => _targetPoints[index];
}
