using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStatusData", menuName = "ScriptableObjects/EnemyStatusData")]
public class EnemyStatusDataSO : ScriptableObject
{
    public StatusData Speed;
    public StatusData Health;
    public StatusData Attack;
    public StatusData Defense;
}

[System.Serializable]
public class EnemyStatus
{
    public float Speed;
    public float MaxHealth;
    public float Attack;
    public float Defense; 

    public EnemyStatus() { }
    public EnemyStatus(EnemyStatus status)
    {
        Speed = status.Speed;
        MaxHealth = status.MaxHealth;
        Attack = status.Attack;
        Defense = status.Defense;
    }
}