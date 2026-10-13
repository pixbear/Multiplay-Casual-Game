using PB.MANAGER;
using PB.SYSTEM;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    private CharacterStatus playerStatus => GameManager.Singleton.TestPlayerStatus;
    private BulletStatus bulletStatus => GameManager.Singleton.TestBulletStatus;

    private float attackTimer;
    private PObjectPoolManager objectPoolManager;

    private void Start()
    {
        objectPoolManager = PObjectPoolManager.Singleton;
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;

        float attackInterval = Mathf.Lerp( 2f, 0.01f, (playerStatus.Attack - 2f) / 78f );

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
            bulletPrefab.Init(bulletStatus, fireDirection);
        }
    }
}