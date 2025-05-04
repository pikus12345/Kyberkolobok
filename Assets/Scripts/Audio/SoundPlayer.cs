using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    public static void PlaySoundWithMixer(AudioClip clip, Vector3 position, float volume = 1f, AudioMixerGroup mixerGroup = null)
    {
        // ������� ��������� GameObject � AudioSource
        GameObject tempSoundObject = new GameObject("TempAudio");
        tempSoundObject.transform.position = position;

        // ��������� AudioSource � �����������
        AudioSource audioSource = tempSoundObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.spatialBlend = 1f; // 3D-����
        audioSource.outputAudioMixerGroup = mixerGroup; // ��������� ������ �������

        // ������������� � ���������� ������ ����� ����������
        audioSource.Play();
        Destroy(tempSoundObject, clip.length);
    }
}