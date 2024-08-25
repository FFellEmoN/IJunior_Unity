using UnityEngine;

public class ProbabilityDivision : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _value;

    public void SetValue(float value)
    {
        _value = value;
    }
    
    public float GetValue()
    {
        return _value;
    }
}
