using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStatusData", menuName = "ScriptableObjects/CharacterStatusData", order = -1000)]
public class CharacterStatusDataSO : ScriptableObject
{
    public float[] Speed = new float[5] { 10f, 15f, 20f, 25f, 30f };
    public float[] MaxHealth = new float[5] { 100f, 150f, 200f, 250f, 300f };
    public float[] Attack = new float[5] { 10f, 15f, 20f, 25f, 30f };
    public float[] Defense = new float[5] { 5f, 10f, 15f, 20f, 25f };
}

[System.Serializable]
public class CharacterStatus
{
    public float Speed;
    public float MaxHealth;
    public float Attack;
    public float Defense; 

    public CharacterStatus() { }
    public CharacterStatus(CharacterStatus status)
    {
        Speed = status.Speed;
        MaxHealth = status.MaxHealth;
        Attack = status.Attack;
        Defense = status.Defense;
    }
}


