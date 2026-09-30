using UnityEngine;
using DG.Tweening;
using System.Collections;

public class EnemyMoveController : MonoBehaviour
{
    private Enemy enemy;
    private Rigidbody rb;
    private EnemyStatus enemyStatus => enemy.EnemyStatus;

    private Transform target;
    private Vector3 moveDirection;
    private bool isJumping = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        target = PlayerManager.Instance.Player.transform;
    }

    private void OnEnable()
    {
        StartCoroutine(JumpRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        DOTween.Kill(transform);
    }

    private void LateUpdate()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        if (target == null) return;
        moveDirection = (target.position - transform.position).normalized;
        moveDirection.y = 0;
        if (moveDirection == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        float speed = enemyStatus.MoveSpeed * Time.fixedDeltaTime;
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, speed);
    }

    private IEnumerator JumpRoutine()
    {
        if (isJumping) yield break;
        isJumping = true;

        float force = enemyStatus.MoveSpeed;
        float interval = Mathf.Max(0.1f, 5f / force);
        float duration = interval * 0.5f;

        yield return new WaitForSeconds(interval);
        yield return transform.DOScale(new Vector3(1.2f, 0.8f, 1.2f), duration).SetEase(Ease.OutQuad).WaitForCompletion();
        yield return new WaitForSeconds(interval);
        yield return transform.DOScale(new Vector3(0.7f, 1.3f, 0.7f), duration).SetEase(Ease.OutBack).WaitForCompletion();
        float forceX = force * moveDirection.x;
        float forceZ = force * moveDirection.z;
        rb.linearVelocity = new Vector3(forceX, force, forceZ);
        yield return transform.DOScale(Vector3.one, duration).SetEase(Ease.OutElastic).WaitForCompletion();
    }

    private IEnumerator LandRoutine()
    {
        float force = enemyStatus.MoveSpeed;
        float interval = Mathf.Max(0.1f, 10f / force);
        float duration = interval * 0.2f;

        yield return transform.DOScale(new Vector3(1.3f, 0.7f, 1.3f), duration).SetEase(Ease.OutQuad).WaitForCompletion();
        yield return transform.DOScale(Vector3.one, duration).SetEase(Ease.OutElastic).WaitForCompletion();

        isJumping = false;
        StartCoroutine(JumpRoutine());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            StartCoroutine(LandRoutine());
    }
}