using System;

[Serializable]
public class EnemyStatus
{
    public readonly float BASE_MAX_HEALTH = 5f;

    public float MaxHealth = 1f;
    public float MoveSpeed = 3f;

    public EnemyStatus() { }
    public EnemyStatus(EnemyStatus status)
    {
        MaxHealth = status.MaxHealth;
        MoveSpeed = status.MoveSpeed;
    }
}