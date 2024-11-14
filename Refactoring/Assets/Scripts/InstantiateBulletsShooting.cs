using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InstantiateBulletsShooting : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeWaitShooting;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _target;

    private Coroutine _coroutine;

    private void OnValidate()
    {
        if (_speed == 0)
        {
            Debug.Log($"{nameof(_speed)} не инициализирован.");
        }

        if (_timeWaitShooting == 0)
        {
            Debug.Log($"{nameof(_timeWaitShooting)} не инициализирован.");
        }

        if (_prefab == null)
        {
            Debug.Log($"{nameof(_prefab)} не инициализирован.");
        }

        if (_target == null)
        {
            Debug.Log($"{nameof(_target)} не инициализирован.");
        }
    }

    private void Start()
    {
        _coroutine = StartCoroutine(_shootingWorker());
    }

    private IEnumerator _shootingWorker()
    {
        WaitForSeconds delay = new WaitForSeconds(_timeWaitShooting);

        while (enabled)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            GameObject newBullet = Instantiate(_prefab, transform.position + direction, Quaternion.identity);

            newBullet.transform.up = direction;
            Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                newBullet.transform.up = direction;
                bulletRigidbody.velocity = direction * _speed;
            }
            else
            {
                Debug.LogError($"{nameof(newBullet)} не имеет Rigidbody.");
            }

            yield return delay;
        }
    }
}