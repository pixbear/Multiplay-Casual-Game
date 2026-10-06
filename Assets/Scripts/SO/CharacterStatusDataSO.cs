using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStatusData", menuName = "ScriptableObjects/CharacterStatusData")]
public class CharacterStatusDataSO : ScriptableObject
{
    public StatusData Speed;
    public StatusData Health;
    public StatusData Attack;
    public StatusData Defense; 
}

[System.Serializable]
public class CharacterStatus
{
    public float Speed;
    public float Health;
    public float Attack;
    public float Defense; 

    public CharacterStatus() { }
    public CharacterStatus(CharacterStatus status)
    {
        Speed = status.Speed;
        Health = status.Health;
        Attack = status.Attack;
        Defense = status.Defense;
    }
}


