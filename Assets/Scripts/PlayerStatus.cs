using System;

[Serializable]
public class PlayerStatus
{
    public float Damage = 4f;
    public float MaxHealth = 3f; // max = 50
    public float AttackSpeed = 2f; // max = 80
    public float MoveSpeed = 5f; // max = 20
    public float Defense = 2f; // max = 10
    public float Mana = 10f; // max = 50
    public float ManaRegenerationRate = 1f; // max = 5
    public float CritChance = 0.1f; // max = 1
    public float CritMultiplier = 2f; // max = 10
    public float DodgeChance = 0.1f; // max = 1

    public PlayerStatus(PlayerStatus playerStatus)
    {
        MaxHealth = playerStatus.MaxHealth;
        AttackSpeed = playerStatus.AttackSpeed;
        MoveSpeed = playerStatus.MoveSpeed;
        Defense = playerStatus.Defense;
        Mana = playerStatus.Mana;
        ManaRegenerationRate = playerStatus.ManaRegenerationRate;
        CritChance = playerStatus.CritChance;
        CritMultiplier = playerStatus.CritMultiplier;
        DodgeChance = playerStatus.DodgeChance;
    }
}
