using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DirectedFlyingCarSpawner : MonoBehaviour
{
    [Header("Car Settings")]
    public GameObject[] carPrefabs;       // Массив моделей машин
    public float minSpeed = 5f;
    public float maxSpeed = 15f;
    public float spawnHeight = 10f;       // Фиксированная высота спавна

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;       // Точки спавна (например, здания)
    public float spawnRate = 2f;
    public int maxCars = 10;
    public float spawnRadius = 5f;        // Разброс вокруг точки спавна

    private List<GameObject> activeCars = new List<GameObject>();

    void Start()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Нет точек спавна! Добавьте Transform-объекты в массив spawnPoints.");
            return;
        }

        StartCoroutine(SpawnCars());
    }

    IEnumerator SpawnCars()
    {
        while (true)
        {
            if (activeCars.Count < maxCars && carPrefabs.Length > 0)
            {
                // Выбираем случайную машину и точку спавна
                GameObject randomCarPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // Позиция спавна (с небольшим случайным смещением)
                Vector3 spawnOffset = new Vector3(
                    Random.Range(-spawnRadius, spawnRadius),
                    0,
                    Random.Range(-spawnRadius, spawnRadius)
                );
                Vector3 spawnPos = spawnPoint.position + spawnOffset;

                // Создаём машину
                GameObject car = Instantiate(randomCarPrefab, spawnPos, Quaternion.identity);
                activeCars.Add(car);

                // Направление движения — "вперёд" от точки спавна (например, от здания)
                Quaternion moveDirection = spawnPoint.rotation;

                // Настраиваем движение
                SetupCarMovement(car, moveDirection);

                // Удаляем из списка при уничтожении
                CarLifecycle lifecycle = car.AddComponent<CarLifecycle>();
                lifecycle.OnDestroyed += () => activeCars.Remove(car);
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        foreach(Transform point in spawnPoints)
        {
            Gizmos.DrawRay(point.position, -point.up*1000f);
        }
    }

    void SetupCarMovement(GameObject car, Quaternion direction)
    {
        Rigidbody rb = car.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float speed = Random.Range(minSpeed, maxSpeed);
            

            // Поворачиваем машину в направлении движения
            if (direction != null)
            {
                car.transform.rotation = direction;
            }
        }
    }
}

public class CarLifecycle : MonoBehaviour
{
    public System.Action OnDestroyed;

    void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}