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
     //  private float _repeatRate = 1f;
        private int _poolCapacity = 5;
        private int _poolMaxSize = 5;

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
            float divider = 2;

            float xPoisition = Random.Range(
                Vector3.zero.x - _mainPlatform.gameObject.transform.localScale.x / divider, 
                Vector3.zero.x + _mainPlatform.gameObject.transform.localScale.x / divider);
            float yPosition = transform.position.y;
            float zPoisition = Random.Range(
                Vector3.zero.z - _mainPlatform.gameObject.transform.localScale.z / divider,
                Vector3.zero.z + _mainPlatform.gameObject.transform.localScale.z / divider);

            return new Vector3(xPoisition, yPosition, zPoisition);
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
            customCube.SetColor(_defaultMaterial);
            customCube.SetWasContactPlane();
        }
    }
}