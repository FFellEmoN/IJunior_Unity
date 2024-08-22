using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CustomCube : MonoBehaviour
{
    [SerializeField] private List<Material> _colorsCubes;
    [SerializeField] private ProbabilityDivision _probabilityCube;

    private Vector3 _localScaleDestroyedCube;
    private Vector3 _transformPositionDestroyedCube;
    private float _pobabilityDestroyedCube;
    private bool _isDestroyed = false;

    private void Start()
    {
        if (_isDestroyed)
        {
            SetLocalScale();
            SetPosition();
            SetProbability();
            SetColor();
        }
    }

    public void SetTrgger()
    {
        _isDestroyed = true;
    }

    public void SetLocalScaleDestroyedCube(Vector3 localScaleDestroyedCube)
    {
        _localScaleDestroyedCube = localScaleDestroyedCube;
    }

    public void SetPositionDestroyedCube(Vector3 transformPositionDestroyedCube)
    {
        _transformPositionDestroyedCube = transformPositionDestroyedCube;
    }

    public void SetProbabilityDestroyedCube(float pobabilityDestroyedCube)
    {
        _pobabilityDestroyedCube = pobabilityDestroyedCube;
    }

    private void SetLocalScale()
    {
        ;
        int dividerHalf = 2;

        transform.localScale = _localScaleDestroyedCube / dividerHalf;
    }

    private void SetPosition()
    {
        transform.position = _transformPositionDestroyedCube;
    }

    private void SetProbability()
    {
        float denominator = 2;

        float newProbability = _pobabilityDestroyedCube / denominator;

        _probabilityCube.SetValue(newProbability);
    }

    private void SetColor()
    {
        if (_colorsCubes.Count > 0)
        {
            int randomValue = Random.Range(0, _colorsCubes.Count);

            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = _colorsCubes[randomValue];
        }
        else
        {
            Debug.Log("Список матерьялов пуст.");
        }
    }
}