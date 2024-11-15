using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft; // 오른손, 왼손 구분을 위한 bool 변수 선언
    public SpriteRenderer spriter;

    SpriteRenderer player; // 플레이어의 스프라이트렌더러 변수 선언

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0); // 오른손의 위치를 Vector3 형태로 저장
    Vector3 rightPosReverse= new Vector3(-0.15f, -0.15f, 0); // 오른손의 위치를 Vector3 형태로 저장
    Quaternion leftRot = Quaternion.Euler(0, 0, -35); // 왼손의 각회전을 Quatanion 형태로 저장
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135); 

    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1]; // 모든 SpriteRenderer를 가지고 올떄 항상 자신의 SpriteRenderer가 [0]에 저장된다. 지금 가지고 올것의 플레이어의 것이므로 1
    }

    void LateUpdate()
    {
        bool isReverse = player.flipX; // 플레이어의 반전 상태를 지역변수로 저장

        if (isLeft) { // 근접 무기
            transform.localRotation = isReverse ? leftRotReverse : leftRot; // 플레이어의 기준으로 Rotate하기 때문에 꼭 localRotation을 사용한다
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6; // 반전되었을떄의 OrderInRayer가 다름, 이미지의 OrderInRayer를 변경해주기 위해 sortingOrder변경
        }
        else { // 원거리 무기
            transform.localPosition = isReverse ? rightPosReverse : rightPos; // 플레이어의 기준으로 회전이므로 localPosition 사용
            spriter.flipX = isReverse; // 오른손 스프라이트는 X축 반전
            spriter.sortingOrder = isReverse ? 6 : 4; // 반전되었을떄의 OrderInRayer가 다름, 이미지의 OrderInRayer를 변경해주기 위해 sortingOrder변경

        }
    }
}
