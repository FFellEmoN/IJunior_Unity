using System.Collections;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private float _maxVolume = 1.0f;
    private float _minVolume = 0.0f;
    private float _fadeDuration = 1.0f;

    private void OnValidate()
    {
        if (_audioSource == null)
        {
            Debug.Log($"{nameof(_audioSource)} не инициализирован.");
        }
    }

    private void Start()
    {
        _audioSource.volume = _minVolume;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Crook>())
        {
            StartCoroutine(FadeAudioVolume(_audioSource.volume, _maxVolume));
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<Crook>())
        {
            StartCoroutine(FadeAudioVolume(_audioSource.volume, _minVolume));
        }
    }

    private IEnumerator FadeAudioVolume(float startVolume, float targetVolume)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < _fadeDuration)
        {
            _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / _fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _audioSource.volume = targetVolume;
    }
}
