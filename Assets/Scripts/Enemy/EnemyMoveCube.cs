using UnityEngine;
using DG.Tweening;
using System.Collections;

public class EnemyMoveCube : EnemyMoveBase
{
    [Header("Jump Settings")]
    [SerializeField] float force = 5f;
    [SerializeField] float jumpHeight = 5f;
    private bool isGround = false;

    private IEnumerator JumpRoutine()
    {
        float speed = enemyStatus.Speed;
        float interval = Mathf.Max(0.01f, 1f / speed);

        yield return new WaitForSeconds(interval);
        yield return transform.DOScale(new Vector3(1.2f, 0.8f, 1.2f), 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();
        yield return new WaitForSeconds(interval);
        float forceX = force * runtimeMoveDir.x;
        float forceZ = force * runtimeMoveDir.z;
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
            ShowLandEffect();
        }
    }

    private void ShowLandEffect()
    {
        PParticlePoolManager.Instance.Show("LandDust", transform.position, 2f);
    }
}