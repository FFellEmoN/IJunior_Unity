using System.Collections;
using UnityEngine;

public class InstantiateBulletsShooter : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeWaitShooting;
    [SerializeField] private Bullet _templetBullet;
    [SerializeField] private Transform _target;

    private Coroutine _coroutine;

    private void OnValidate()
    {
        if (_speed >= 0)
        {
            Debug.Log($"{nameof(_speed)} не инициализирован.");
        }

        if (_timeWaitShooting == 0)
        {
            Debug.Log($"{nameof(_timeWaitShooting)} не инициализирован.");
        }

        if (_templetBullet == null)
        {
            Debug.Log($"{nameof(_templetBullet)} не инициализирован.");
        }

        if (_target == null)
        {
            Debug.Log($"{nameof(_target)} не инициализирован.");
        }
    }

    private void Start()
    {
        _coroutine = StartCoroutine(ShootingWorker());
    }

    private IEnumerator ShootingWorker()
    {
        WaitForSeconds delay = new WaitForSeconds(_timeWaitShooting);

        while (enabled)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            Bullet newBullet = Instantiate(_templetBullet, transform.position + direction, Quaternion.identity);

            newBullet.gameObject.transform.up = direction;
            Rigidbody bulletRigidbody = newBullet.GetRigidbody();

            newBullet.gameObject.transform.up = direction;
            bulletRigidbody.velocity = direction * _speed;

            yield return delay;
        }
    }
}