using PB.SYSTEM;
using UnityEngine;

public class GameManager : PMonoSingleton<GameManager>
{
    public bool IsTestMode { get; private set; } = false;
    
}
