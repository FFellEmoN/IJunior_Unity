using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CubeCreator : MonoBehaviour
{
    [SerializeField] private List<UnityEngine.Color> _colorsCubes;
    private int _divider = 2;

    private void Start()
    {
        _colorsCubes.Add(new UnityEngine.Color(1f, 0f, 0f, 1f));
        _colorsCubes.Add(new UnityEngine.Color(0f, 0f, 0f, 1f));
        _colorsCubes.Add(new UnityEngine.Color(0f, 0f, 1f, 1f));
    }

    public void Create(GameObject destroyedCube)
    {
        // Создаем куб
        CustomCube customCube = new CustomCube();
    }
}
