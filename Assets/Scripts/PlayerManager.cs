using PB.SYSTEM;
using UnityEngine;

public class PlayerManager : PMonoSingleton<PlayerManager>
{
    public Player Player;
    public Transform PlayerTransform;

    [SerializeField] PlayerStatus playerStatus; 
    public PlayerStatus PlayerStatus => playerStatus; 
    [SerializeField] BulletStatus bulletStatus; 
    public BulletStatus BulletStatus => bulletStatus; 
}
