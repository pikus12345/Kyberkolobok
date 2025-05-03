using UnityEngine;

public class TimeSlowdown : MonoBehaviour
{
    [Header("Настройки замедления времени")]
    [Range(0f, 1f)] public float targetTimeScale = 0f; // 0 - остановка, 1 - нормальная скорость
    public float slowdownDuration = 2f; // Время замедления в секундах (реальном времени)
    public AnimationCurve slowdownCurve = AnimationCurve.EaseInOut(0, 1, 1, 0); // Кривая замедления
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
            // Плавное изменение временного масштаба
            currentSlowdownTime += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(currentSlowdownTime / slowdownDuration);
            float curveValue = slowdownCurve.Evaluate(progress);

            Time.timeScale = Mathf.Lerp(initialTimeScale, targetTimeScale, curveValue);
            Time.fixedDeltaTime = 0.02f * Time.timeScale; // Корректируем fixedDeltaTime

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
    // Запуск замедления времени
    public void StartSlowdown()
    {
        initialTimeScale = Time.timeScale;
        currentSlowdownTime = 0f;
        isSlowingDown = true;
    }

    // Сброс времени к нормальной скорости
    public void ResetTime()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        isSlowingDown = false;
    }

    // Вызывается при полной остановке времени
    private void OnSlowdownComplete()
    {
        Debug.Log("Время достигло целевого масштаба: " + Time.timeScale);
        // Здесь можно добавить дополнительные действия
    }

    // Пример использования (можно вызывать из других скриптов или событий)
    private void ExampleUsage()
    {
        // Начать плавное замедление
        StartSlowdown();

        // Восстановить время через 5 секунд (в реальном времени)
        Invoke("ResetTime", 5f);
    }
}