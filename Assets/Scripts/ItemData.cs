using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")] // CreateAssetMenu = 커스텀 메뉴를 생성하는 속성
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal } // 근접공격, 원거리공격, 장갑, 신발, 체력회복
    
    [Header("# Main Info")] // 아이템의 각종 속성들을 변수로 작성하기
    public ItemType itemType;
    public int itemId; 
    public string itemName;
    public string itemDec; 
    public Sprite itemIcon; // 아이템 아이콘

    [Header("# Level Data")]
    public float baseDamage; // 0레벨 일때의 기본 공격력
    public float baseCount; // 0레벨 일때의 기본 카운트(관통, 근접공격속도)
    public float[] damage;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile; // 투사체
}
