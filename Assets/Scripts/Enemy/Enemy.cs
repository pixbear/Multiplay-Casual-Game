using System.Collections;
using DG.Tweening;
using PB.MANAGER;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStatus EnemyStatus => enemyStatus;
    [SerializeField] private EnemyStatus enemyStatus;
    [SerializeField] GameObject flashEffect;
    [SerializeField] float flashDuration = 0.2f;

    public void Set(EnemyStatus enemyStatus)
    {
        this.enemyStatus = new EnemyStatus(enemyStatus);
    }

    public void GetDamage(float damage)
    {
        enemyStatus.MaxHealth -= damage;
        StartCoroutine(FlashEffectRoutine());

        if (enemyStatus.MaxHealth <= 0)
        {
            OnDie();
        }
    }

    private void OnDisable()
    {
        // Reset
        transform.localScale = Vector3.one;
        flashEffect.SetActive(false);
        StopAllCoroutines();
        DOTween.Kill(transform);
    }

    private void OnDie()
    {
        StopAllCoroutines();
        PObjectPoolManager.Instance.Release("Enemy", gameObject);
    }

    private IEnumerator FlashEffectRoutine()
    {
        flashEffect.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        flashEffect.SetActive(false);
    }
}
