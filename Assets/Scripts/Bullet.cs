using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bullet : MonoBehaviour
{ // 근접무기 프리펩의 Order in Layer를 몬스터보다 높이기
    public float damage;
    public int per; // 관통

    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;
    }
}
