using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCreator : MonoBehaviour
{
    private int _divider = 2;
    public void Create(Vector3 position, Vector3 size)
    {
        // Создаем куб
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Устанавливаем позицию куба
        cube.transform.position = position;

        // Устанавливаем размер куба
        cube.transform.localScale = size / _divider;

        // Добавляем компонент Rigidbody, чтобы куб стал физическим объектом
        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.mass = 1f;
    }
}
