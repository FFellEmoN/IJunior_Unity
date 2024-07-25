using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCube : MonoBehaviour
{
    private float _cubeSize;// нужно ли поле или можно сделать локлальным

    private GameObject _cube;
    private GameObject _destroyedCube;
    private Color _cubeColor;

    private MeshRenderer meshRenderer;

    public CustomCube(GameObject destroyCube, Color color)
    {
        _destroyedCube = destroyCube;
        _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _cubeColor = color;
        SetPosition();
        SetLocalScale();
        SetColor();

        // ----------
        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.mass = 1f;
    }

    private void SetLocalScale()
    {
        _cube.transform.localScale = new Vector3(_cubeSize, _cubeSize, _cubeSize);
    }

    private void SetColor()
    {
        _cube.GetComponent<MeshRenderer>().material.color = _cubeColor;
    }

    private void SetPosition()
    {
        _cube.transform.position = _destroyedCube.transform.position;
    }
}