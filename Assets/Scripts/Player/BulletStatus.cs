using System;
using UnityEngine;

[Serializable]
public class BulletStatus
{
    public float BulletSpeed = 10f; // 총알 속도
    public float BulletDamage = 1f; // 총알 데미지
    public int PierceCount = 0; // 관통 수
    public int BulletCount = 1; // 발사되는 총알 수
    public float KnockbackPower = 0f; // 밀치는  힘

    public BulletStatus() { }
    public BulletStatus(BulletStatus status)
    {
        BulletSpeed = status.BulletSpeed;
        BulletDamage = status.BulletDamage;
        PierceCount = status.PierceCount;
        BulletCount = status.BulletCount;
        KnockbackPower = status.KnockbackPower;
    }

}
