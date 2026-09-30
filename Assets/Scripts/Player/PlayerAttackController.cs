using PB.MANAGER;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    private PlayerStatus playerStatus => PlayerManager.Instance.PlayerStatus;
    private BulletStatus bulletStatus => PlayerManager.Instance.BulletStatus;

    private float attackTimer;
    private PObjectPoolManager objectPoolManager;

    private void Start()
    {
        objectPoolManager = PObjectPoolManager.Instance;
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;

        float attackInterval = Mathf.Lerp( 2f, 0.01f, (playerStatus.AttackSpeed - 2f) / 78f );

        attackInterval = Mathf.Round(attackInterval * 100f) / 100f;

        if (attackTimer >= attackInterval)
        {
            attackTimer = 0f;
            Fire();
        }
    }

    private void Fire()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(distance);

            Vector3 fireDirection = mouseWorldPosition - transform.position;
            fireDirection.y = 0f;
            fireDirection.Normalize();

            Vector3 firePosition = transform.position + fireDirection;
            Bullet bulletPrefab = objectPoolManager.Get<Bullet>("Bullet", firePosition);
            bulletPrefab.Set(bulletStatus);
            bulletPrefab.Shot(fireDirection);
        }
    }
}