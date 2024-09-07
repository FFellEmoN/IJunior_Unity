using System.Collections.Generic;
using UnityEngine;

public class CustomCube : MonoBehaviour
{
    [SerializeField] private List<Material> _colorsCubes;
    [SerializeField] private ExplosionRadius _explosionRadiusCube;

    private float _probabilityCube = 100f;
    private float _explosionRadius = 3;

    private void Start()
    {
        SetColor();
    }

    public void Init(Vector3 localScaleDestroyedCube,
        Vector3 transformPositionDestroyedCube,
        float pobabilityDestroyedCube,
        float explosionRadiusDestroyedCube)
    {
        SetLocalScale(localScaleDestroyedCube);
        SetPosition(transformPositionDestroyedCube);
        SetProbability(pobabilityDestroyedCube);
        SetExplosionRadius(explosionRadiusDestroyedCube);
    }

    public float GetProbability()
    {
        return _probabilityCube;
    }

    public float GetExplosionRadius()
    {
        return _explosionRadius;
    }

    private void SetLocalScale(Vector3 localScaleDestroyedCube)
    {
        int dividerHalf = 2;

        transform.localScale = localScaleDestroyedCube / dividerHalf;
    }

    private void SetPosition(Vector3 transformPositionDestroyedCube)
    {
        transform.position = transformPositionDestroyedCube;
    }

    private void SetProbability(float probabilityCube)
    {
        float denominator = 2;
        _probabilityCube = probabilityCube / denominator;
    }

    private void SetExplosionRadius(float explosionRadiusDestroyedCube)
    {
        float multiplier = 2;
        _explosionRadius = explosionRadiusDestroyedCube * multiplier;
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