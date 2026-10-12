using UnityEngine;

public class EnemyMoveSphere : EnemyMoveBase
{
    [Header("시각적 구르기 설정")]
    [SerializeField] private Transform body; // 굴려줄 자식 메쉬 오브젝트를 인스펙터에서 드래그 앤 드롭 하세요.

    private void FixedUpdate()
    {
        rb.linearVelocity = runtimeMoveDir * enemyStatus.Speed;
        body.Rotate(Vector3.right, runtimeSpeed * 100f * Time.fixedDeltaTime, Space.Self);
    }
}
