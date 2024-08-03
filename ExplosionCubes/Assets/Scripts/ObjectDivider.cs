using System;
using UnityEngine;

public class ObjectDivider : MonoBehaviour
{
    [SerializeField] private EmittedBeam _emittedBeam;

    public event Action<CustomCube> CreatedCustomCube;
    private int _minNumberCubs = 2;
    private int _maxNumberCubs = 7;
    private float _maxProcents = 100f;

    private void OnEnable()
    {
            _emittedBeam.BeamHitObject += TryDivide;
    }

    private void OnDisable()
    {
            _emittedBeam.BeamHitObject -= TryDivide;
    }

    public void TryDivide(GameObject destroyedCube)
    {
        float randomChance = UnityEngine.Random.Range(0, _maxProcents);
        float valueProbality = destroyedCube.GetComponent<ProbabilityDivision>().GetValue();

        if (randomChance < valueProbality)
        {
            Destroy(destroyedCube);

            int numberCubs = UnityEngine.Random.Range(_minNumberCubs, _maxNumberCubs);

            for (int i = 0; i < numberCubs; i++)
            {
                CustomCube newCustomCube = new CustomCube(destroyedCube, valueProbality);
                CreatedCustomCube?.Invoke(newCustomCube);
            }
        }
        else
        {
            Debug.Log(this.name + " - Объект не делится. Вероятность " + valueProbality + "%, случайное число: " + randomChance);

            Destroy(destroyedCube);
        }
    }
}