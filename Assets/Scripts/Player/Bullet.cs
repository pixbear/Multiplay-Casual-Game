using PB.MANAGER;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public BulletStatus BulletStatus => bulletStatus;
    private BulletStatus bulletStatus;
    private Vector3 targetDir;
    

    public void Init(BulletStatus status, Vector3 dir)
    {
        bulletStatus = new BulletStatus(status);
        targetDir = dir;
        rb.linearVelocity = targetDir.normalized * bulletStatus.Speed;
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
            enemyRb.AddForce(targetDir * bulletStatus.KnockbackPower, ForceMode.VelocityChange);
    }

    private void Hit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.GetDamage(bulletStatus.Damage);
            bulletStatus.PierceCount--;
            if (bulletStatus.PierceCount <= 0)
            {
                PObjectPoolManager.Instance.Release("Bullet", this.gameObject);
            }
        }
    }
}
