using PB.SYSTEM;
using UnityEngine;

public class GameManager : PMonoSingleton<GameManager>
{
    [SerializeField] bool isTestMode;
    public bool IsTestMode => isTestMode;
  
}
