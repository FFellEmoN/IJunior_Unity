using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRadius : MonoBehaviour
{
    [SerializeField] private float _value;

    public void SetValue(float value)
    {
        _value = value;
    }

    public float GetValue()
    {
        return _value;
    }
}