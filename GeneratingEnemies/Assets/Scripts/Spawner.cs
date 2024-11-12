using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private Coroutine _coroutine;

    private void OnValidate()
    {
        if (_spawnPoints == null && isActiveAndEnabled == true)
        {
            Debug.Log($"{nameof(_spawnPoints)} не инициализирован.");
        }
    }

    private void Start()
    {
        float delayedAppearance = 2f;

        _coroutine = StartCoroutine(Countdown(delayedAppearance));
    }

    private IEnumerator Countdown(float delay)
    {
        WaitForSecondsRealtime delayTime = new WaitForSecondsRealtime(delay);

        while (enabled)
        {
            SpawnPoint spawnPoint = ChoosingRandomSpawnPoint();
            spawnPoint.Spawn();

            yield return delayTime;
        }
    }

    private SpawnPoint ChoosingRandomSpawnPoint()
    {
        int numberRandomSpawnPoint = Random.Range(0, _spawnPoints.Count);

        return _spawnPoints[numberRandomSpawnPoint];
    }
}
