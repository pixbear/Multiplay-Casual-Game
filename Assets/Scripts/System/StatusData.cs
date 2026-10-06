using UnityEngine;
using System;

[Serializable]
public class StatusData
{
    public float[] value;
    public int GetMaxLevel()
    {
        return value != null ? value.Length : 0;
    }
    public float GetValueByLevel(int level)
    {
        if (value == null || level < 1 || level > value.Length)
        {
            Debug.LogError($"Invalid level: {level}");
            return 0f;
        }
        return value[level - 1];
    }
}