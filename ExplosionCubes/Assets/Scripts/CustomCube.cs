using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCube
{
    [SerializeField] private int _probabilityDivision;

    private GameObject _cube;
    private GameObject _destroyedCube;
    private float _mass = 1;

    public CustomCube(GameObject destroyedCube, float probabilityDestroyedCube)
    {
        _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _destroyedCube = destroyedCube;
        
        SetLocalScale();
        SetPosition();
       // SetColor(color);
        SetTag();
        SetProbability(probabilityDestroyedCube);

        _cube.AddComponent<DividableObject>();
       
        Rigidbody rb = _cube.AddComponent<Rigidbody>();

        rb.mass = _mass;
    }

    public GameObject Get()
    {
        return _cube;
    }

    public void SetColor(Material color)
    {
        MeshRenderer meshRenderer = _cube.GetComponent<MeshRenderer>();
        meshRenderer.material = color;
    }

    private void SetLocalScale()
    {
        Vector3 sizeDestroyCube = _destroyedCube.transform.localScale;
        int dividerHalf = 2;
        _cube.transform.localScale = sizeDestroyCube / dividerHalf;
    }

    private void SetPosition()
    {
        _cube.transform.position = _destroyedCube.transform.position;
    }

    private void SetTag()
    {
        _cube.tag = _destroyedCube.tag;
    }

    private void SetProbability(float probability)
    {
        float denominator = 2;
        ProbabilityDivision probabilityCube = _cube.AddComponent<ProbabilityDivision>();
        float newProbability = probability / denominator;

        probabilityCube.SetValue(newProbability);
    }
}