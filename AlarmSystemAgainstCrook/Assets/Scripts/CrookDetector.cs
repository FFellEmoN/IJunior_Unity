using UnityEngine;

public class CrookDetector : MonoBehaviour
{
    [SerializeField] private VolumeController _volumeController;

    private Coroutine _coroutine;

    private void OnValidate()
    {
        if (_volumeController == null)
        {
            Debug.LogError($"{nameof(_volumeController)} не инициализирован.");
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Crook>())
        {
            float maxVolume = 1;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(_volumeController.FadeAudioVolume(maxVolume));
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<Crook>())
        {
            float minVolume = 0;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(_volumeController.FadeAudioVolume(minVolume));
        }
    }
}
