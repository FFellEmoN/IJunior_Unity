using System.Collections.Generic;
using UnityEngine;

public class CustomCube : MonoBehaviour
{
    [SerializeField] private List<Material> _colors;

    private float _probability = 100f;
    private float _explosionRadius = 3;

    public float ExplosionRadius
    {
        get
        {
            return _explosionRadius;
        }

        private set
        {
            float multiplier = 2;
            _explosionRadius = value * multiplier;
        }
    }

    public float Probability
    {
        get
        {
            return _probability;
        }

        private set
        {
            float denominator = 2;
            _probability = value / denominator;
        }
    }

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
        Probability = pobabilityDestroyedCube;
        ExplosionRadius = explosionRadiusDestroyedCube;
    }

    public Collider GetCollider()
    {
        return GetComponent<Collider>();
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

    private void SetColor()
    {
        if (_colors.Count > 0)
        {
            int randomValue = Random.Range(0, _colors.Count);

            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = _colors[randomValue];
        }
        else
        {
            Debug.Log($"{nameof(_colors)} is eampty.");
        }
    }
}