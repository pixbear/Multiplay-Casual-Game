using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStatusData", menuName = "ScriptableObjects/EnemyStatusData", order = -1000)]
public class EnemyStatusDataSO : ScriptableObject
{
    public float[] Speed = new float[5] { 10f, 15f, 20f, 25f, 30f };
    public int[] MaxHealth = new int[5] { 100, 150, 200, 250, 300 };
    public int[] Attack = new int[5] { 10, 15, 20, 25, 30 };
    public int[] Defense = new int[5] { 5, 10, 15, 20, 25 };
}

[System.Serializable]
public class EnemyStatus
{
    public float Speed;
    public int MaxHealth;
    public int Attack;
    public int Defense; 

    public EnemyStatus() { }
    public EnemyStatus(EnemyStatus status)
    {
        Speed = status.Speed;
        MaxHealth = status.MaxHealth;
        Attack = status.Attack;
        Defense = status.Defense;
    }
}