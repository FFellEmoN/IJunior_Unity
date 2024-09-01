using System.Collections.Generic;
using UnityEngine;

public class CustomCube : MonoBehaviour
{
    [SerializeField] private List<Material> _colorsCubes;
    [SerializeField] private ProbabilityDivision _probabilityCube;
    [SerializeField] private ExplosionRadius _explosionRadiusCube;

    private void Start()
    {
        SetColor();
    }

    public void SetLocalScale(Vector3 localScaleDestroyedCube)
    {
        int dividerHalf = 2;

        transform.localScale = localScaleDestroyedCube / dividerHalf;
    }

    public void SetPosition(Vector3 transformPositionDestroyedCube)
    {
        transform.position = transformPositionDestroyedCube;
    }

    public void SetProbability(float pobabilityDestroyedCube)
    {
        if (_probabilityCube != null) {
            float denominator = 2;
            float newProbability = pobabilityDestroyedCube / denominator;

            _probabilityCube.SetValue(newProbability);
        }
        else
        {
            Debug.Log($"{nameof(_probabilityCube)} = null");
        }
    }

    public void SetExplosionRadius(float explosionRadiusDestroyedCube)
    {
        if (_explosionRadiusCube != null)
        {
            float multiplier = 2;
            float newExplosionRadius = explosionRadiusDestroyedCube * multiplier;

            _explosionRadiusCube.SetValue(newExplosionRadius);
        }
        else
        {
            Debug.Log($"{nameof(_explosionRadiusCube)} = null");
        }
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
            Debug.Log($"{nameof(_colorsCubes)} is eampty.");
        }
    }
}