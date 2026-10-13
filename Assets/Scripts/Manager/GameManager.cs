using PB.SYSTEM;
using UnityEngine;

public class GameManager : PMonoSingleton<GameManager>
{
    [SerializeField] bool isTestMode;
    public bool IsTestMode => isTestMode;


    [SerializeField] Player player;
    public Player Player => player;
    

    [Header("Test Status")]
    [SerializeField] CharacterStatus testPlayerStatus;
    public CharacterStatus TestPlayerStatus => testPlayerStatus;
    [SerializeField] BulletStatus testBulletStatus;
    public BulletStatus TestBulletStatus => testBulletStatus;

    [ContextMenu("Set Bullet Status to Default")]
    private void ResetBulletStatus() => testBulletStatus = new BulletStatus();

    [ContextMenu("Set Player Status to Default")]
    private void ResetPlayerStatus() => testPlayerStatus = new CharacterStatus();

    private void Start()
    {
        FollowCam cam = Camera.main.GetComponent<FollowCam>();
        if (cam != null) cam.SetTarget(player.transform);
    }
}