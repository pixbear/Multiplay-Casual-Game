using PB.SYSTEM;
using UnityEngine;

public class PlayerManager : PMonoSingleton<PlayerManager>
{
    public Player Player;

    [SerializeField] PlayerStatus playerStatus;
    public PlayerStatus PlayerStatus => playerStatus;
    [SerializeField] BulletStatus bulletStatus;
    public BulletStatus BulletStatus => bulletStatus;

    [ContextMenu("Set Bullet Status to Default")]
    private void ResetBulletStatus() => bulletStatus = new BulletStatus();

    [ContextMenu("Set Player Status to Default")]
    private void ResetPlayerStatus() => playerStatus = new PlayerStatus();
}
