using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private void OnValidate()
    {
        if (_rigidbody == null)
        {
            Debug.Log($"{nameof(_rigidbody)} не инициализирован.");
        }
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }
}
