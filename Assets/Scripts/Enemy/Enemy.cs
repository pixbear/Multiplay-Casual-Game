using System.Collections;
using DG.Tweening;
using PB.MANAGER;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected virtual string key => "Name";
    private GameObject flashEffect;
    private EnemyStatus enemyStatus;
    public EnemyStatus EnemyStatus => enemyStatus;
    private float flashDuration = 0.1f;
    private int curHp;
    private Transform target = null;
    public Transform Target => target;

    private void Awake()
    {
        flashEffect = transform.GetChild(0).gameObject;
        flashEffect.SetActive(false);
    }

    private void LateUpdate()
    {
        if (target != null) return;
        FindTarget();
    }

    public void Set(EnemyStatus status)
    {
        enemyStatus = new EnemyStatus(status);
        curHp = enemyStatus.MaxHealth;
    }

    public void GetDamage(int damage)
    {
        curHp -= damage;
        StartCoroutine(FlashEffectRoutine());

        if (curHp <= 0)
        {
            OnDie();
        }
    }

    private void FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        if (targets == null || targets.Length == 0)
        {
            target = null;
            return;
        }

        GameObject nearestEnemy = null;
        float closestDis = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            Vector3 directionToEnemy = target.transform.position - transform.position;
            float disToEnemy = directionToEnemy.sqrMagnitude;

            if (disToEnemy < closestDis)
            {
                closestDis = disToEnemy;
                nearestEnemy = target;
            }
        }

        target = nearestEnemy.transform;
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
        var key = string.Format("Enemy_{0}", this.key);
        PObjectPoolManager.Singleton.Release(key, gameObject);
    }

    private IEnumerator FlashEffectRoutine()
    {
        flashEffect.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        flashEffect.SetActive(false);
    }
}
