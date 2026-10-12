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

    private void Awake()
    {
        flashEffect = transform.GetChild(0).gameObject;
        flashEffect.SetActive(false);
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
        PObjectPoolManager.Instance.Release(key, gameObject);
    }

    private IEnumerator FlashEffectRoutine()
    {
        flashEffect.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        flashEffect.SetActive(false);
    }
}
