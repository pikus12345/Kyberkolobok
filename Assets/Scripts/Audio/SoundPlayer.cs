using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    public static void PlaySoundWithMixer(AudioClip clip, Vector3 position, float volume = 1f, AudioMixerGroup mixerGroup = null)
    {
        // Создаем временный GameObject с AudioSource
        GameObject tempSoundObject = new GameObject("TempAudio");
        tempSoundObject.transform.position = position;

        // Добавляем AudioSource и настраиваем
        AudioSource audioSource = tempSoundObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.spatialBlend = 1f; // 3D-звук
        audioSource.outputAudioMixerGroup = mixerGroup; // Назначаем группу микшера

        // Воспроизводим и уничтожаем объект после завершения
        audioSource.Play();
        Destroy(tempSoundObject, clip.length);
    }
}