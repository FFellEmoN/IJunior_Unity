using UnityEngine;
using UnityEngine.Pool;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private MovementCube _target;
    [SerializeField] private Enemy _prefabEnemy;

    private ObjectPool<Enemy> _pool;

    private void OnValidate()
    {
        if (isActiveAndEnabled == true) {
            if (_target == null)
            {
                Debug.Log($"{nameof(_target)} не инициализирован.");
            }

            if (_prefabEnemy == null)
            {
                Debug.Log($"{nameof(_prefabEnemy)} не инициализирован.");
            }
        }
    }

    private void Awake()
    {
        bool isCheckPool = true;

        int poolCapacity = 15;
        int poolMaxSize = 15;

        _pool = new ObjectPool<Enemy>(
                createFunc: () => Instantiate(_prefabEnemy),
                actionOnGet: (enemy) => Prepare(enemy),
                actionOnRelease: (enemy) => Deactive(enemy),
                actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
                collectionCheck: isCheckPool,
                defaultCapacity: poolCapacity,
                maxSize: poolMaxSize);
    }

    public void Spawn()
    {
        _pool.Get();
    }

    private void Enable(Enemy enemy)
    {
        enemy.AchievedTarget += OnAchievedTarget;
    }

    private void Disable(Enemy enemy)
    {
        enemy.AchievedTarget -= OnAchievedTarget;
    }

    private void Prepare(Enemy enemy)
    {
        Enable(enemy);
        enemy.gameObject.SetActive(true);
        enemy.SetTarget(_target);
        enemy.gameObject.transform.position = transform.position + Vector3.up;
    }

    private void Deactive(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnAchievedTarget(Enemy enemy)
    {
        Disable(enemy);
        _pool.Release(enemy);
    }
}
