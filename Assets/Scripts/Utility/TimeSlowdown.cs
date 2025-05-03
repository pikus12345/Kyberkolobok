using UnityEngine;

public class TimeSlowdown : MonoBehaviour
{
    [Header("��������� ���������� �������")]
    [Range(0f, 1f)] public float targetTimeScale = 0f; // 0 - ���������, 1 - ���������� ��������
    public float slowdownDuration = 2f; // ����� ���������� � �������� (�������� �������)
    public AnimationCurve slowdownCurve = AnimationCurve.EaseInOut(0, 1, 1, 0); // ������ ����������
    AudioSource[] allAudioSources;

    private float initialTimeScale;
    private float currentSlowdownTime;
    private bool isSlowingDown;

    void Start()
    {
        allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (isSlowingDown)
        {
            // ������� ��������� ���������� ��������
            currentSlowdownTime += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(currentSlowdownTime / slowdownDuration);
            float curveValue = slowdownCurve.Evaluate(progress);

            Time.timeScale = Mathf.Lerp(initialTimeScale, targetTimeScale, curveValue);
            Time.fixedDeltaTime = 0.02f * Time.timeScale; // ������������ fixedDeltaTime

            foreach (AudioSource src in allAudioSources)
            {
                src.pitch = Time.timeScale;
            }

            if (progress >= 1f)
            {
                isSlowingDown = false;
                OnSlowdownComplete();
            }
        }
        
    }
    // ������ ���������� �������
    public void StartSlowdown()
    {
        initialTimeScale = Time.timeScale;
        currentSlowdownTime = 0f;
        isSlowingDown = true;
    }

    // ����� ������� � ���������� ��������
    public void ResetTime()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        isSlowingDown = false;
    }

    // ���������� ��� ������ ��������� �������
    private void OnSlowdownComplete()
    {
        Debug.Log("����� �������� �������� ��������: " + Time.timeScale);
        // ����� ����� �������� �������������� ��������
    }

    // ������ ������������� (����� �������� �� ������ �������� ��� �������)
    private void ExampleUsage()
    {
        // ������ ������� ����������
        StartSlowdown();

        // ������������ ����� ����� 5 ������ (� �������� �������)
        Invoke("ResetTime", 5f);
    }
}