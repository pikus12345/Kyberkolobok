using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float visionRadius = 10f; // Радиус обнаружения игрока
    [SerializeField] private float chaseSpeed = 3.5f; // Скорость преследования
    [SerializeField] private float patrolSpeed = 2f; // Скорость патрулирования
    [SerializeField] private float stoppingDistance = 1f; // Дистанция остановки перед игроком
    [SerializeField] private float SpeedChangeRate = 10.0f;

    [SerializeField] private Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    private Transform player; // Ссылка на игрока
    private NavMeshAgent agent;
    private Vector3 startPosition; // Стартовая позиция для возврата
    private bool isChasing = false;
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Игрок должен иметь тег "Player"
        startPosition = transform.position;
        animator = GetComponent<Animator>();

        // Настройка NavMeshAgent
        agent.speed = patrolSpeed;
        agent.stoppingDistance = stoppingDistance;
    }

    private void Update()
    {
        float targetSpeed = 0;
        // Проверяем, виден ли игрок
        if (CanSeePlayer())
        {
            // Преследуем игрока
            isChasing = true;
            targetSpeed = chaseSpeed;
            agent.SetDestination(player.position);
        }
        else
        {
            // Возвращаемся на стартовую позицию
            isChasing = false;
            targetSpeed = patrolSpeed;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            if (agent.remainingDistance < 0.5f)
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        agent.speed = Mathf.Lerp(agent.speed, targetSpeed, Time.deltaTime * SpeedChangeRate);
        animator.SetFloat("MotionSpeed", agent.speed);
        animator.SetFloat("Speed", agent.speed);

        // (Опционально) Остановка, если уже у стартовой точки
        //if (!isChasing && Vector3.Distance(transform.position, startPosition) < 0.5f)
        //{
        //    agent.isStopped = true;
        //}
        //else
        //{
        //    agent.isStopped = false;
        //}
    }

    // Проверка видимости игрока
    private bool CanSeePlayer()
    {

        // Проверка расстояния
        if (Vector3.Distance(transform.position, player.position) > visionRadius)
            return false;
        else 
            return true;

        // Проверка на прямой видимость (без препятствий)
        /*RaycastHit hit;
        Vector3 direction = player.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit, visionRadius,6))
        {
            if (hit.transform == player)
            {
                Debug.Log("Вижу игрока!");
                return true;
            }
        }

        return false;*/
    }

    // Визуализация радиуса в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }
}