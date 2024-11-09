using Unity.VisualScripting;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>(); // Collider2D는 기본도형의 모든 콜라이더 2D를 포함
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) // Area태그가 아니면 함수를 실행하지 않겠다
            return; // return 키워드를 만나면 더이상 실행하지 않고 함수 탈출

        Vector3 playerPos = GameManager.instance.player.transform.position;
        // player를 직접 할당받지 않고 GameManager의 instance를 통해 받게한다
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x); // Mathf.Abs = 음수도 양수로 만들어주는 절대값 함수
        float diffY = Mathf.Abs(playerPos.y - myPos.y);
    
        Vector3 playerDir = GameManager.instance.player.inputVec; // 플레이어의 이동 방향을 저장하기 위한 변수 추가
        float dirX = playerDir.x < 0 ? -1 : 1; 
        // 대각선일 때는 Normalized에 의해 1보다 작은 값이 되어버림 = 값을 조정할 필요가 있다
        // 3항 연산자 (조건) ? (true일 때 값) : (false일 때 값)
        float dirY = playerDir.y < 0 ? -1 : 1; 

        switch (transform.tag) { // switch ~ case = 값의 상태에 땨라 로직을 나눠주는 키워드
            case "Ground": // 플레이어의 이동에 따른 맵의 위치를 재조정
                if (diffX > diffY) { // 두 오브젝트의 거리차이에서 X축이 Y축보다 크면 수평이동
                    transform.Translate(Vector3.right * dirX * 40); 
                    // Translate = 지정된 값만큼 현재위치에서 이동
                    // Vector3.right = (1, 0, 0)
                    // 한개의 map의 크기인 20이 아닌 40을 이동해야 하는것 확인
                }
                else if (diffX < diffY) {
                    transform.Translate(Vector3.up * dirY * 40); 
                    // vector3.up = (1, 0, 0)
                }
                break;
            case "Enemy": // 적과 너무 멀어졌을때 적의 위치를 재조정하기 위한 코드
                if (coll.enabled) { // 적이 죽었을 경우 collider를 비활성화 할것
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0)); 
                    // 플레이어의 이동방향에 따라 맞은편에서 등장하도록 이동
                    // 플레이어의 위치를 기준으로 랜덤으로 조금 떨어진 곳에서 생성
                }
                break;    
        }
    }
}
