using System;

[Serializable]
public class PlayerStatus
{
    public float Damage = 4f;
    public float MaxHealth = 3f; // max = 50
    public float AttackSpeed = 2f; // max = 80
    public float MoveSpeed = 5f; // max = 20

    public PlayerStatus(PlayerStatus playerStatus)
    {
        MaxHealth = playerStatus.MaxHealth;
        AttackSpeed = playerStatus.AttackSpeed;
        MoveSpeed = playerStatus.MoveSpeed;
    }
}
