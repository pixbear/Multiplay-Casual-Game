using UnityEngine;

public class EnemyMoveBase : MonoBehaviour
{
    private EnemyBase enemy;
    protected Rigidbody rb;
    protected EnemyStatus enemyStatus => enemy.EnemyStatus;

    protected Transform target;
    protected Vector3 targetDir;
    protected Vector3 runtimeMoveDir;
    protected float runtimeSpeed;

  
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemy = GetComponent<EnemyBase>();
        rb.sleepThreshold = 0f;
    }

    private void Start()
    {
        target = GameManager.Instance.Player.transform;
    }

    private void LateUpdate()
    {
        LookAtTarget();
        runtimeSpeed = rb.linearVelocity.magnitude;
        runtimeMoveDir = transform.forward;
    }

    private void LookAtTarget()
    {
        if (target == null) return;
        targetDir = (target.position - transform.position).normalized;
        targetDir.y = 0;
        if (targetDir == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        float speed = enemyStatus.Speed * Time.fixedDeltaTime;
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, speed * 0.1f));
    }
}