using UnityEngine;
using UnityEngine.Pool;

namespace CubesRain
{
    public class SpawnerCubes : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameObject _mainPlatform;
        [SerializeField] private Material _defaultMaterial;

        private ObjectPool<GameObject> _pool;
        private int _poolCapacity = 5;
        private int _poolMaxSize = 15;

        private Vector3 _startPoint;

        private void Awake()
        {
            _pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(_prefab),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => ActionOnRelease(obj),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: false,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize);
        }

        private void Start()
        {
            InvokeRepeating(nameof(GetCube), 0, 0.3f);
        }

        private void GetCube()
        {
            _pool.Get();
        }

        private void ActionOnGet(GameObject cube)
        {
            cube.transform.SetPositionAndRotation(
                CalculateRandomSpawnPosition(), 
                Quaternion.Euler(CalculateRandomRotation()));
            cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
            cube.gameObject.SetActive(true);
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
            gameObject.gameObject.SetActive(false);
            CustomCube customCube = gameObject.GetComponent<CustomCube>();
            customCube.SetWasContactPlane();
        }
    }
}