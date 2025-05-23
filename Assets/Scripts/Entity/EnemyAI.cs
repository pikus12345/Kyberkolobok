
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("���������")]
    [SerializeField] private float visionRadius = 10f; // ������ ����������� ������
    [SerializeField] private float chaseSpeed = 3.5f; // �������� �������������
    [SerializeField] private float patrolSpeed = 2f; // �������� ��������������
    [SerializeField] private float stoppingDistance = 1f; // ��������� ��������� ����� �������
    [SerializeField] private float SpeedChangeRate = 10.0f;

    [Header("����� �����")]
    [SerializeField] private AudioClip[] FootstepAudioClips;
    [Range(0f, 1f)][SerializeField] private float FootstepAudioVolume;

    [Header("�����")]
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private AudioClip[] AttackClips;
    [Range(0f, 1f)][SerializeField] private float AttackAudioVolume;

    [Header("��������������")]
    [SerializeField] private Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    

    private Transform player; // ������ �� ������
    private NavMeshAgent agent;
    private Vector3 startPosition; // ��������� ������� ��� ��������
    private bool isChasing = false;
    private Animator animator;

    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // ����� ������ ����� ��� "Player"
        startPosition = transform.position;
        animator = GetComponent<Animator>();

        // ��������� NavMeshAgent
        agent.speed = patrolSpeed;
        agent.stoppingDistance = stoppingDistance;
    }

    private void Update()
    {
        float targetSpeed = 0;
        if (animator.GetBool("Attack")) return;
        // ���������, ����� �� �����
        if (CanSeePlayer())
        {
            // ���������� ������
                isChasing = true;
                targetSpeed = chaseSpeed;
                agent.SetDestination(player.position);
                agent.isStopped = false;
                if (agent.remainingDistance <= stoppingDistance)
                {
                    agent.isStopped = true;
                    Attack();
                }

        }
        else
        {
            // ������������ �� ��������� �������
            AttackEnded();
            isChasing = false;
            targetSpeed = patrolSpeed;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            agent.isStopped = false;
            if (agent.remainingDistance < 0.5f)
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        agent.speed = Mathf.Lerp(agent.speed, targetSpeed, Time.deltaTime * SpeedChangeRate);
        animator.SetFloat("MotionSpeed", agent.speed);
        animator.SetFloat("Speed", agent.speed);

        // (�����������) ���������, ���� ��� � ��������� �����
        //if (!isChasing && Vector3.Distance(transform.position, startPosition) < 0.5f)
        //{
        //    agent.isStopped = true;
        //}
        //else
        //{
        //    agent.isStopped = false;
        //}
    }
    private void Attack()
    {
        animator.SetBool("Attack", true);
    }
    private void AttackSound()
    {
        if (AttackClips.Length > 0)
        {
            var index = Random.Range(0, AttackClips.Length);
            SoundPlayer.PlaySoundWithMixer(AttackClips[index], attackPoint.position, AttackAudioVolume);
        }
    }
    private void OnAttack()
    {
        var v = Physics.CheckSphere(attackPoint.position,
                attackRadius, playerLayer, QueryTriggerInteraction.Ignore);
        if (v){
            GameManager.instance.PlayerDeath();
        }
    }
    public void AttackEnded()
    {
        animator.SetBool("Attack", false);
    }

    // �������� ��������� ������
    private bool CanSeePlayer()
    {

        // �������� ����������
        if (Vector3.Distance(transform.position, player.position) > visionRadius)
            return false;
        else 
            return true;

        // �������� �� ������ ��������� (��� �����������)
        /*RaycastHit hit;
        Vector3 direction = player.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit, visionRadius,6))
        {
            if (hit.transform == player)
            {
                Debug.Log("���� ������!");
                return true;
            }
        }

        return false;*/
    }

    // ������������ ������� � ���������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f,0,0, 0.5f);
        Gizmos.DrawWireSphere(transform.position, visionRadius);

        if (attackPoint)
        {
            Gizmos.DrawSphere(
                attackPoint.position,
                attackRadius);
        }
        
    }
    private void OnFootstep()
    {
        if (FootstepAudioClips.Length > 0)
        {
            var index = Random.Range(0, FootstepAudioClips.Length);
            SoundPlayer.PlaySoundWithMixer(FootstepAudioClips[index], transform.position, FootstepAudioVolume);
        }
    }
}