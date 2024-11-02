using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace CubesRain
{
    public class SpawnerCubes : MonoBehaviour
    {
        [SerializeField] private CustomCube _prefab;
        [SerializeField] private GameObject _mainPlatformOnScene;

        private ObjectPool<CustomCube> _pool;
        private Coroutine _coroutine;

        private void OnValidate()
        {
            if (_prefab == null)
            {
                Debug.LogError($"{nameof(_prefab)} не установлен в {nameof(CubesRain)} в {gameObject.name}");
            }

            if (_mainPlatformOnScene == null)
            {
                Debug.Log($"{nameof(_mainPlatformOnScene)} = null");
            }
        }

        private void Awake()
        {
            bool _isCheckPool = true;
            int _poolCapacity = 15;
            int _poolMaxSize = 15;

            _pool = new ObjectPool<CustomCube>(
                    createFunc: () => Instantiate(_prefab),
                    actionOnGet: (cube) => Prepare(cube),
                    actionOnRelease: (cube) => ResetZero(cube),
                    actionOnDestroy: (cube) => Destroy(cube.gameObject),
                    collectionCheck: _isCheckPool,
                    defaultCapacity: _poolCapacity,
                    maxSize: _poolMaxSize);
        }

        private void Start()
        {
            float intensityRain = 0.3f;

            _coroutine = StartCoroutine(Countdown(intensityRain));
        }

        private void Enable(CustomCube customCube)
        {
            customCube.TimeRelese += OnTimeRelease;
        }

        private void Disable(CustomCube customCube)
        {
            customCube.TimeRelese -= OnTimeRelease;
        }

        private void OnTimeRelease(CustomCube customCube)
        {
            Disable(customCube);
            _pool.Release(customCube);
        }

        private void Spawn()
        {
            _pool.Get();
        }

        private void Prepare(CustomCube customCube)
        {
            customCube.Init(true);
            customCube.SetActive(true);
            Enable(customCube);
            customCube.transform.SetPositionAndRotation(
                CalculateRandomSpawnPosition(),
                Quaternion.Euler(CalculateRandomRotation()));
        }

        private Vector3 CalculateRandomSpawnPosition()
        {
            float multiplier = 4;
            float xPosition = Random.Range(
                _mainPlatformOnScene.transform.position.x - _mainPlatformOnScene.transform.localScale.x * multiplier,
                _mainPlatformOnScene.transform.position.x + _mainPlatformOnScene.transform.localScale.x * multiplier);
            float yPosition = _mainPlatformOnScene.transform.position.y + transform.position.y;
            float zPosition = Random.Range(
                _mainPlatformOnScene.transform.position.z - _mainPlatformOnScene.transform.localScale.z * multiplier,
                _mainPlatformOnScene.transform.position.z + _mainPlatformOnScene.transform.localScale.z * multiplier);

            return new Vector3(xPosition, yPosition, zPosition);
        }

        private Vector3 CalculateRandomRotation()
        {
            float minAngle = 0;
            float maxAngle = 360;
            float xRotation = Random.Range(minAngle, maxAngle);
            float yRotation = Random.Range(minAngle, maxAngle);
            float zRotation = Random.Range(minAngle, maxAngle);

            return new Vector3(xRotation, yRotation, zRotation);
        }

        private void ResetZero(CustomCube customCube)
        {
            customCube.Init(false);
            customCube.SetActive(false);
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
    }
}