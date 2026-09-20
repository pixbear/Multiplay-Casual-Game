using PB.MANAGER;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStatus EnemyStatus=> enemyStatus;
    private EnemyStatus enemyStatus;
    
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
    }

    private void Die()
    {
        PObjectPoolManager.Instance.Release("Enemy", this.gameObject);
    }
}
