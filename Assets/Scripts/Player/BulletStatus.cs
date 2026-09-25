using System;

[Serializable]
public class BulletStatus 
{
    public float BulletSpeed = 10f;
    public float BulletDamage = 1f;
    public int PierceCount = 0;
    public int BulletCount = 1; 
    public float KnockbackPower = 0f; 

    public BulletStatus(BulletStatus status)
    {
        BulletSpeed = status.BulletSpeed;
        BulletDamage = status.BulletDamage;
        PierceCount = status.PierceCount;
        BulletCount = status.BulletCount;
        KnockbackPower = status.KnockbackPower;
    }
}
