using PB.SYSTEM;
using UnityEngine;

public class PlayerManager : PMonoSingleton<PlayerManager>
{
    public Player Player;

    [Header("Test Status")]
    [SerializeField] CharacterStatus testPlayerStatus;
    public CharacterStatus TestPlayerStatus => testPlayerStatus;
    [SerializeField] BulletStatus testBulletStatus;
    public BulletStatus TestBulletStatus => testBulletStatus;

    [ContextMenu("Set Bullet Status to Default")]
    private void ResetBulletStatus() => testBulletStatus = new BulletStatus();

    [ContextMenu("Set Player Status to Default")]
    private void ResetPlayerStatus() => testPlayerStatus = new CharacterStatus();
}
