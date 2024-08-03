using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingColor : MonoBehaviour
{
    [SerializeField] private List<Material> _colorsCubes;
    [SerializeField] private ObjectDivider _obvectDivider;

    private void OnEnable()
    {
        _obvectDivider.CreatedCustomCube += GenerateColor;
    }

    private void OnDisable()
    {
        _obvectDivider.CreatedCustomCube -= GenerateColor;
    }

    private void GenerateColor(CustomCube customCube)
    {
        int randomValue = Random.Range(0, _colorsCubes.Count);

        customCube.SetColor(_colorsCubes[randomValue]);
    }
}