using UnityEngine;
using UnityEngine.Pool;

namespace CubesRain
{
    public class SpawnerCubes : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameObject _mainPlatform;

        private ObjectPool<GameObject> _pool;
        private int _poolCapacity = 15;
        private int _poolMaxSize = 30;
        private bool _isCheckPool = true;

        public void ReleaseCube(GameObject cube)
        {
            _pool.Release(cube);
        }

        private void OnValidate()
        {
            if (_prefab == null)
            {
                Debug.LogError($"{nameof(_prefab)} не установлен в {nameof(CubesRain)} в {gameObject.name}");
            }

            if (_mainPlatform == null)
            {
                Debug.Log($"{nameof(_mainPlatform)} = null");
            }
        }

        private void Awake()
        {
            _pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(_prefab),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => ActionOnRelease(obj),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: _isCheckPool,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize);
        }

        private void Start()
        {
            float repeatRate = 0.5f;

            InvokeRepeating(nameof(GetCube), 0, repeatRate);
        }

        private void GetCube()
        {
            _pool.Get();
        }

        private void ActionOnGet(GameObject cube)
        {
            cube.SetActive(true);
            cube.transform.SetPositionAndRotation(
                CalculateRandomSpawnPosition(),
                Quaternion.Euler(CalculateRandomRotation()));

            if (cube.GetComponent<Rigidbody>())
            {
                cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        private Vector3 CalculateRandomSpawnPosition()
        {
            float multiplier = 4;
            float xPosition = Random.Range(
                _mainPlatform.transform.position.x - _mainPlatform.transform.localScale.x * multiplier,
                _mainPlatform.transform.position.x + _mainPlatform.transform.localScale.x * multiplier);
            float yPosition = _mainPlatform.transform.position.y + transform.position.y;
            float zPosition = Random.Range(
                _mainPlatform.transform.position.z - _mainPlatform.transform.localScale.z * multiplier,
                _mainPlatform.transform.position.z + _mainPlatform.transform.localScale.z * multiplier);

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

        private void ActionOnRelease(GameObject gameObject)
        {
            gameObject.SetActive(false);

            if (gameObject.GetComponent<CustomCube>())
            {
                CustomCube customCube = gameObject.GetComponent<CustomCube>();

                customCube.SetWasContactPlane();
                customCube.SetStandardColor();
            }
        }
    }
}