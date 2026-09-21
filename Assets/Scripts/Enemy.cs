using System.Collections;
using PB.MANAGER;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStatus EnemyStatus => enemyStatus;
    private EnemyStatus enemyStatus;
    private Renderer enemyRenderer;

    public void Set(EnemyStatus enemyStatus)
    {
        this.enemyStatus = new EnemyStatus(enemyStatus);
    }

    public void GetDamage(float damage)
    {
        enemyStatus.MaxHealth -= damage;
        if (enemyStatus.MaxHealth <= 0)
        {
            Die();
        }
        StartCoroutine(FlashWhite());
    }

    private void Die()
    {
        PObjectPoolManager.Instance.Release("Enemy", this.gameObject);
    }

    IEnumerator FlashWhite()
    {
        enemyRenderer = GetComponent<Renderer>();
        enemyRenderer.material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        enemyRenderer.material.color = enemyRenderer.material.color;
    }
}
