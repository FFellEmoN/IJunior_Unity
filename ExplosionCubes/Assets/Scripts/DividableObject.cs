using UnityEngine;

public class DividableObject : MonoBehaviour
{
    private EmittedBeam _emittedBeam;
    private int _minNumberCubs = 2;
    private int _maxNumberCubs = 7;
    private float _maxProcents = 100f;

    private void Start()
    {
        GameObject emittedBeam = GameObject.Find("Emitted Beam");

        if (emittedBeam != null)
        {
            Debug.Log(" obj инициализирован");
            _emittedBeam = emittedBeam.GetComponent<EmittedBeam>();
        }
    }

    private void OnEnable()
    {
        if (_emittedBeam != null)
        {
            Debug.Log("подписались");
            _emittedBeam.OnCollisionChange += TryDivide;
        }
    }

    private void OnDisable()
    {
        _emittedBeam.OnCollisionChange -= TryDivide;
    }

    public void TryDivide(GameObject destroyedCube)
    {
        float randomChance = Random.Range(0, _maxProcents);
        float valueProbality = destroyedCube.GetComponent<ProbabilityDivision>().GetValue();

        if (randomChance < valueProbality)
        {
            int numberCubs = Random.Range(_minNumberCubs, _maxNumberCubs);

            for (int i = 0; i < numberCubs; i++)
            {
                new CustomCube(destroyedCube, valueProbality);
            }
        }
        else
        {
            Debug.Log(this.name + " - Объект не делится. Вероятность " + valueProbality + "%, случайное число: " + randomChance);
        }
    }
}