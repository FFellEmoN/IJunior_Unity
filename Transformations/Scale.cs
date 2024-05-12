using UnityEngine;

public class Scale : MonoBehaviour
{
    private float _speedScale = 0.005f;
    private Vector3 scaleChange;

    private void Awake()
    {
        scaleChange = new Vector3(-_speedScale, -_speedScale, -_speedScale);
    }

    void Update()
    {
        transform.localScale += scaleChange;

        if (transform.localScale.y < 0.1f || transform.localScale.y > 1.0f)
        {
            scaleChange = -scaleChange;
        }
    }
}