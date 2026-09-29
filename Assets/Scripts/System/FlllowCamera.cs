using UnityEngine;
using UnityEngine.InputSystem;

public class FlllowCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0f, 3f, -6f);

    [SerializeField] bool useAimCamera;
    [SerializeField] private float aimStrength = 2f;
    [SerializeField] private float maxAimOffset = 2f;
    [SerializeField] private bool useSmooth = false;
    [SerializeField] private float smoothSpeed = 8f;

    private Transform target;

    private void Start()
    {
        target = PlayerManager.Instance.Player.transform;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 mouseOffset = Vector3.zero;

        if (useAimCamera)
        {
            Vector2 mouseViewportPosition = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
            Vector2 mouseFromCenter = (mouseViewportPosition - new Vector2(0.5f, 0.5f)) * 2f;
            mouseOffset = new Vector3(mouseFromCenter.x * aimStrength, 0f, mouseFromCenter.y * aimStrength);
            mouseOffset = Vector3.ClampMagnitude(mouseOffset, maxAimOffset);
        }

        Vector3 desiredPosition = target.position + offset + mouseOffset;
        desiredPosition.y = target.position.y + offset.y;

        if (useSmooth)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        }
        else
        {
            transform.position = desiredPosition;
        }
    }
}
