using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCube
{
    [SerializeField] private int _probabilityDivision;

    private GameObject _newCube;
    private GameObject _destroyedCube;
    private float _mass = 1;

    public CustomCube(GameObject destroyedCube, float probabilityDestroyedCube)
    {
        _newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _destroyedCube = destroyedCube;
        
        SetLocalScale();
        SetPosition();
        SetTag();
        SetProbability(probabilityDestroyedCube);
       
        Rigidbody rb = _newCube.AddComponent<Rigidbody>();
        rb.mass = _mass;
    }

    public void SetColor(Material color)
    {
        MeshRenderer meshRenderer = _newCube.GetComponent<MeshRenderer>();
        meshRenderer.material = color;
    }

    private void SetLocalScale()
    {
        Vector3 sizeDestroyCube = _destroyedCube.transform.localScale;
        int dividerHalf = 2;
        _newCube.transform.localScale = sizeDestroyCube / dividerHalf;
    }

    private void SetPosition()
    {
        _newCube.transform.position = _destroyedCube.transform.position;
    }

    private void SetTag()
    {
        _newCube.tag = _destroyedCube.tag;
    }

    private void SetProbability(float probability)
    {
        float denominator = 2;
        ProbabilityDivision probabilityCube = _newCube.AddComponent<ProbabilityDivision>();
        float newProbability = probability / denominator;

        probabilityCube.SetValue(newProbability);
    }
}