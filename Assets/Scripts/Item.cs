using UnityEngine;
using UnityEngine.UI; // UI 컴포넌트(Image)를 쓰기위해서 사용해야하는 에셋

public class Item : MonoBehaviour
{
    public ItemData data; // 아이템 관리에 필요한 변수들 선언
    public int level;
    public Weapon weapon;

    Image icon;
    Text textLevel;

    void Awake() 
    {
        // 자식 오브젝트의 컴포넌트가 필요하므로 GetComponetsInChilden 사용       
        // GetComponentsInChildren에서 두번째 값으로 가져오기 (첫번째는 자기자신)
        icon = GetComponentsInChildren<Image>()[1]; 
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0]; 
    }

    void LateUpdate() // LateUpdate에서 레벨 텍스트 갱신
    {
        textLevel.text = "Lv." + (level + 1); // 레벨을 0이 아닌 1부터 시작 
    }
}
