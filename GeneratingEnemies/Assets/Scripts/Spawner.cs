using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private Enemy _prefabEnemy;

    private Coroutine _coroutine;
    private ObjectPool<Enemy> _pool;

    private void OnValidate()
    {
        if (_spawnPoints == null)
        {
            Debug.Log($"{nameof(_spawnPoints)} не инициализирован.");
        }

        if (_prefabEnemy == null)
        {
            Debug.Log($"{nameof(_prefabEnemy)} не инициализирован.");
        }
    }

    private void Awake()
    {
        bool _isCheckPool = true;

        int _poolCapacity = 15;
        int _poolMaxSize = 15;

        _pool = new ObjectPool<Enemy>(
                createFunc: () => Instantiate(_prefabEnemy),
                actionOnGet: (enemy) => Prepare(enemy),
                actionOnRelease: (enemy) => Deactive(enemy),
                actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
                collectionCheck: _isCheckPool,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize);
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
            Spawn();

            yield return delayTime;
        }
    }

    private SpawnPoint ChoosingRandomSpawnPoint()
    {
        int numberRandomSpawnPoint = Random.Range(0, _spawnPoints.Count);

        return _spawnPoints[numberRandomSpawnPoint];
    }

    private void Prepare(Enemy enemy)
    {
        SpawnPoint spawnPoint = ChoosingRandomSpawnPoint();

        Enable(enemy);
        enemy.gameObject.SetActive(true);
        enemy.SetDirectionMovement(spawnPoint.GetDirection());
        enemy.gameObject.transform.position = spawnPoint.transform.position + Vector3.up;
    }

    private void Spawn()
    {
        _pool.Get();
    }

    private void Enable(Enemy enemy)
    {
        enemy.Fell += OnFell;
    }

    private void Disable(Enemy enemy)
    {
        enemy.Fell -= OnFell;
    }

    private void OnFell(Enemy enemy)
    {
        Disable(enemy);
        _pool.Release(enemy);
    }

    private void Deactive(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }
}
