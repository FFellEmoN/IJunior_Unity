using UnityEngine;
using UnityEngine.Pool;

namespace CubesRain
{
    public class SpawnerCubes : MonoBehaviour
    {
        [SerializeField] private CustomCube _prefab;
        [SerializeField] private GameObject _mainPlatformOnScene;
        [SerializeField] private Material _standardMaterial;

        private ObjectPool<CustomCube> _pool;

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

            if (_standardMaterial == null)
            {
                Debug.Log($"{nameof(_standardMaterial)} = null");
            }
        }

        private void Awake()
        {
            bool _isCheckPool = true;
            int _poolCapacity = 15;
            int _poolMaxSize = 15;

            _pool = new ObjectPool<CustomCube>(
                    createFunc: () => Instantiate(_prefab),
                    actionOnGet: (obj) => ActionGet(obj),
                    actionOnRelease: (obj) => ActionRelease(obj),
                    actionOnDestroy: (obj) => Destroy(obj),
                    collectionCheck: _isCheckPool,
                    defaultCapacity: _poolCapacity,
                    maxSize: _poolMaxSize);
        }

        private void Start()
        {
            float intensityRain = 0.3f;

            InvokeRepeating(nameof(GetCube), 0, intensityRain);
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

        private void GetCube()
        {
            _pool.Get();
        }

        private void ActionGet(CustomCube customCube)
        {
            customCube.SetActive(true);
            Enable(customCube);
            customCube.transform.SetPositionAndRotation(
                CalculateRandomSpawnPosition(),
                Quaternion.Euler(CalculateRandomRotation()));

            if (customCube.GetComponent<Rigidbody>())
            {
                customCube.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
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

        private void ActionRelease(CustomCube customCube)
        {
            customCube.SetActive(false);
            customCube.SetWasContactPlane();
            customCube.SetStandardColor(_standardMaterial);
        }
    }
}