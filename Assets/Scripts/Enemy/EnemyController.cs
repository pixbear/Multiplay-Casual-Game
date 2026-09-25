using UnityEngine;

public class EnemyController : MonoBehaviour
{    
    private Enemy enemy;
    private Rigidbody rb;
    private Transform target = null;
    private EnemyStatus enemyStatus => enemy.EnemyStatus;
    private Vector3 moveDirection;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();
        enemy = transform.GetComponent<Enemy>();
    }

    private void Start()
    {
        target = PlayerManager.Instance.PlayerTransform;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            FollowTarget();
            LookAtTarget();
        }
    }

    private void FollowTarget()
    {
        moveDirection = (target.position - transform.position).normalized;
        rb.MovePosition(rb.position + moveDirection * enemyStatus.MoveSpeed * Time.deltaTime);
    }

    private void LookAtTarget()
    {
        if (moveDirection == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, enemyStatus.MoveSpeed * Time.deltaTime);
    }
}
