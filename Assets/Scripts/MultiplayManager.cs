using Unity.Netcode;

public class MultiplayManager : NetworkBehaviour
{
    public static MultiplayManager Singleton { get; private set; }
    private void Awake()
    {
        Singleton = this;
    }

    public void StartHost()
    {
        NetworkManager.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.StartClient();
    }

    public void StopClient()
    {
        NetworkManager.Shutdown();
    }
}
