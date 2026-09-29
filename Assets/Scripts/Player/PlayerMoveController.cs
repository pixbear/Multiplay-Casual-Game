using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float moveSpeed => PlayerManager.Instance.PlayerStatus.MoveSpeed;
    private float rotationSpeed => moveSpeed * 2f;
    private Vector3 moveDirection;

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector2 input = Vector2.zero;
        if (Keyboard.current.wKey.isPressed) input.y += 1;
        if (Keyboard.current.sKey.isPressed) input.y -= 1;
        if (Keyboard.current.aKey.isPressed) input.x -= 1;
        if (Keyboard.current.dKey.isPressed) input.x += 1;
        
        moveDirection = new Vector3(input.x, 0f, input.y).normalized;
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (moveDirection == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}