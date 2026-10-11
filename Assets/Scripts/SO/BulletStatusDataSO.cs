using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletStatusData", menuName = "ScriptableObjects/BulletStatusData", order = -1000)]
public class BulletStatusDataSO : ScriptableObject
{
    public float[] Speed = new float[5] { 10f, 15f, 20f, 25f, 30f };
    public float[] Damage = new float[5] { 1f, 2f, 3f, 4f, 5f };
    public int[] PierceCount = new int[5] { 0, 1, 2, 3, 4 };
    public int[] BulletCount = new int[5] { 1, 2, 3, 4, 5 };
    public float[] KnockbackPower = new float[5] { 0f, 1f, 2f, 3f, 4f };
    public float[] Size = new float[5] { 1f, 1.5f, 2f, 2.5f, 3f };
}

[System.Serializable]
public class BulletStatus
{
    public float Speed; // 총알 속도
    public float Damage; // 총알 데미지
    public int PierceCount; // 관통 수
    public int BulletCount; // 발사되는 총알 수
    public float KnockbackPower; // 밀치는  힘
    public float Size; // 총알 크기

    public BulletStatus() { }
    public BulletStatus(BulletStatus status)
    {
        Speed = status.Speed;
        Damage = status.Damage;
        PierceCount = status.PierceCount;
        BulletCount = status.BulletCount;
        KnockbackPower = status.KnockbackPower;
        Size = status.Size;
    }
}