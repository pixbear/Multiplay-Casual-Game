using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : NetworkBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float moveSpeed => GameManager.Singleton.TestPlayerStatus.Speed;
    private Vector3 moveDirection;
    private float moveAnimationTime;


    private void Update()
    {
        if (IsOwner == false) return;
        ReadInput();
    }

    private void FixedUpdate()
    {
        if (IsOwner == false) return;
        Move();
        Rotate();
    }

    private void LateUpdate()
    {
        if (IsOwner == false) return;
        MoveAnimation();
    }

    private void ReadInput()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) input.y += 1;
        if (Keyboard.current.sKey.isPressed) input.y -= 1;
        if (Keyboard.current.aKey.isPressed) input.x -= 1;
        if (Keyboard.current.dKey.isPressed) input.x += 1;

        moveDirection = new Vector3(input.x, 0f, input.y).normalized;
    }

    private void Move()
    {
        float speed = moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection * speed);
    }

    private void Rotate()
    {
        if (moveDirection.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        float speed = moveSpeed * 2f * Time.fixedDeltaTime;
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, speed);
    }

    private void MoveAnimation()
    {
        if (moveDirection == Vector3.zero)
        {
            float speed = moveSpeed * Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, speed);
            moveAnimationTime = 0f;
            return;
        }

        moveAnimationTime += moveSpeed * 0.1f * Time.deltaTime;
        float stretchAmount = Mathf.PingPong( moveAnimationTime,0.2f);
        float x = 1f + stretchAmount;
        float y = 1f - stretchAmount;
        float z = 1f + stretchAmount;
        transform.localScale = new Vector3(x, y, z);
    }
}