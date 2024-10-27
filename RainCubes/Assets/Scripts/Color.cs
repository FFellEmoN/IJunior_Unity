using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color : MonoBehaviour
{
    [SerializeField] private List<Material> _colors;
    [SerializeField] private Material _defaultMaterial;

    private void OnValidate()
    {
        if (_colors.Count == 0)
        {
            Debug.Log($"{nameof(_colors)} is eampty.");
        }

        if (_defaultMaterial == null)
        {
            Debug.Log($"{nameof(_defaultMaterial)} is eampty.");
        }
    }

    public Material GetRandom()
    {
        int randomValue = UnityEngine.Random.Range(0, _colors.Count);

        return _colors[randomValue];
    }

    public Material GetDefault()
    {
        return _defaultMaterial;
    }
}
