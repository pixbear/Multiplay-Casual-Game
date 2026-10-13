using PB.BUTTON;
using UnityEngine;

public class MultiplayUI : MonoBehaviour
{
    [SerializeField] PButton startHostButton;
    [SerializeField] PButton startClientButton;
    [SerializeField] PButton stopClientButton;

    private void Start()
    {
        startHostButton.onClick.AddListener(() => MultiplayManager.Singleton.StartHost());
        startClientButton.onClick.AddListener(() => MultiplayManager.Singleton.StartClient());
        stopClientButton.onClick.AddListener(() => MultiplayManager.Singleton.StopClient());
    }
}
