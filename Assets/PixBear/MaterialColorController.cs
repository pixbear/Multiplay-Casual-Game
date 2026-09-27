using UnityEngine;

public class MaterialColorController : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;

    private Renderer targetRenderer;

    private void Awake()
    {
        targetRenderer = GetComponent<Renderer>();
        ApplyColor();
    }

    private void OnValidate()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        ApplyColor();
    }

    private void ApplyColor()
    {
        if (targetRenderer == null)
            return;

        targetRenderer.material.SetColor("_BaseColor", color);
    }
}