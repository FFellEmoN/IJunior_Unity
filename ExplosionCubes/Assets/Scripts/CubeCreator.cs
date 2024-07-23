using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CubeCreator : MonoBehaviour
{
    [SerializeField] private List<UnityEngine.Color> _colorsCubes;
    private int _divider = 2;

    public void Create(GameObject destroyedCube)
    {
        // Создаем куб
        GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Устанавливаем позицию куба
        cube.transform.position = destroyedCube.transform.position;

        // Устанавливаем размер куба
        SetSize(destroyedCube, newCube);

        // Добавляем компонент Rigidbody, чтобы куб стал физическим объектом
        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.mass = 1f;
    }

    private void SetSize(GameObject destroyedCube, GameObject newCube)
    {
        newCube.transform.localScale = destroyedCube.transform.localScale / _divider;
    }
}
