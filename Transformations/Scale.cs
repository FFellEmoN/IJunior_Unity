using UnityEngine;

public class Scale : MonoBehaviour
{
    private float _speedScale = 0.005f;
    private Vector3 scaleChange;

    private void Awake()
    {
        scaleChange = new Vector3(-_speedScale, -_speedScale, -_speedScale);
    }

    private void Update()
    {
        float minSize = 0.1f;
        float maxSize = 1.0f;

        transform.localScale += scaleChange;

        if (transform.localScale.y < minSize || transform.localScale.y > maxSize)
        {
            scaleChange = -scaleChange;
        }
    }
}