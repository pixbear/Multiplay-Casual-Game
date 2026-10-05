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
    private bool isGround = false;

    [Header("Jump Settings")]
    [SerializeField] float force = 5f;
    [SerializeField] float jumpHeight = 5f;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        target = PlayerManager.Instance.Player.transform;
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
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, speed * Time.fixedDeltaTime));
        Debug.Log(Time.fixedDeltaTime);
    }

    private IEnumerator JumpRoutine()
    {
        float speed = enemyStatus.MoveSpeed;
        float interval = Mathf.Max(0.01f, 1f / speed);

        yield return new WaitForSeconds(interval);
        yield return transform.DOScale(new Vector3(1.2f, 0.8f, 1.2f), 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();
        yield return new WaitForSeconds(interval);
        float forceX = force * moveDirection.x;
        float forceZ = force * moveDirection.z;
        rb.linearVelocity = new Vector3(forceX, jumpHeight, forceZ);
        isGround = false;
        yield return transform.DOScale(new Vector3(0.8f, 1.2f, 0.8f), 0.3f).SetEase(Ease.OutBack).WaitForCompletion();
        yield return transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();
    }

    private IEnumerator LandRoutine()
    {
        yield return transform.DOScale(new Vector3(1.4f, 0.6f, 1.4f), 0.3f).SetEase(Ease.OutBack).WaitForCompletion();
        yield return transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).WaitForCompletion();
        isGround = true;

        yield return StartCoroutine(JumpRoutine());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGround) return;
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(LandRoutine());
        }
    }
}