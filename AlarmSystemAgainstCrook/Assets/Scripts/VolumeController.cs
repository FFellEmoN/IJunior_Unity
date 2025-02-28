using System.Collections;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnValidate()
    {
        if (_audioSource == null)
        {
            Debug.LogError($"{nameof(_audioSource)} не инициализирован.");
        }
    }

    public IEnumerator FadeAudioVolume(float targetVolume)
    {
        float startVolume = _audioSource.volume;
        float fadeDuration = 1.0f;

        for (float elapsedTime = 0.0f; elapsedTime < fadeDuration; elapsedTime += Time.deltaTime)
        {
            _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeDuration);
            yield return null; 
        }

        _audioSource.volume = targetVolume;
    }
}
