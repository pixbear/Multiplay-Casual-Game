using UnityEngine;

public class FlllowCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private Transform target = null;

    private void Start()
    {
        target = PlayerManager.Instance.PlayerTransform;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
