using System.Collections;
using PB.MANAGER;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStatus EnemyStatus => enemyStatus;
    private EnemyMoveController controller;
    [SerializeField] private EnemyStatus enemyStatus;
    [SerializeField] GameObject flashEffect;
    [SerializeField] float flashDuration = 0.2f;

    private void Awake()
    {
        controller = GetComponent<EnemyMoveController>();
    }

    public void Set(EnemyStatus enemyStatus)
    {
        this.enemyStatus = new EnemyStatus(enemyStatus);
        Reset();
    }

    private void Reset()
    {
        flashEffect.SetActive(false);
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
