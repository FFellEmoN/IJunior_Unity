using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerColor : MonoBehaviour
{
    [SerializeField] private List<Material> _colors;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private MeshRenderer _matirialCube;

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

    public void SetRandom()
    {
        int randomValue = UnityEngine.Random.Range(0, _colors.Count);
        
        _matirialCube.material = _colors[randomValue];
    }

    public void SetDefault()
    {
        _matirialCube.material = _defaultMaterial;
    }
}
