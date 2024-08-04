using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private List<Material> _colorsCubes;
    [SerializeField] private ObjectDivider _obvectDivider;

    private void OnEnable()
    {
        _obvectDivider.CreatedCustomCube += OnCreatedCustomCube;
    }

    private void OnDisable()
    {
        _obvectDivider.CreatedCustomCube -= OnCreatedCustomCube;
    }

    private void OnCreatedCustomCube(CustomCube customCube)
    {
        if (_colorsCubes.Count > 0)
        {
            int randomValue = Random.Range(0, _colorsCubes.Count);

            customCube.SetColor(_colorsCubes[randomValue]);
        }
        else
        {
            Debug.Log("Список матерьялов пуст.");
        }
    }
}