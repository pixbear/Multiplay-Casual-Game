using PB.MANAGER;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public BulletStatus BulletStatus => bulletStatus;
    private BulletStatus bulletStatus;

    public void Set(BulletStatus bulletStatus)
    {
        this.bulletStatus = new BulletStatus(bulletStatus);
    }

    public void Shot(Vector3 direction)
    {
        rb.linearVelocity = direction.normalized * bulletStatus.BulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Knockback(other);
            Hit(other);
        }
        else if (other.CompareTag("Wall"))
        {
            PObjectPoolManager.Instance.Release("Bullet", this.gameObject);
        }
    }

    private void Knockback(Collider other)
    {
        Rigidbody enemyRb = other.GetComponent<Rigidbody>();
        if (enemyRb != null)
        {
            Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
            enemyRb.AddForce(knockbackDirection * bulletStatus.KnockbackPower, ForceMode.Impulse);
        }
    }

    private void Hit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.GetDamage(bulletStatus.BulletDamage);
            bulletStatus.PierceCount--;
            if (bulletStatus.PierceCount <= 0)
            {
                PObjectPoolManager.Instance.Release("Bullet", this.gameObject);
            }
        }
    }
}
