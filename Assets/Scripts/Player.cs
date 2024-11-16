using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed; 
    public Scanner scanner; // 플레이어 스크립트에서 Scanner클래스 타입 변수 선언 및 초기화
    public Hand[] hands; // 플레이어에서 손 스크립트를 담을 배열변수

    Rigidbody2D rigid;
    SpriteRenderer spriter; // SpriteRenderer 값을 받아올 변수
    Animator anim; 

    void Awake ()
    {
        rigid = GetComponent<Rigidbody2D>(); // GetComponent<컴포넌트 이름> = 오브젝트에서 컴포넌트를 가져오는 함수
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true); // 비활성화된 오브젝트는 GetComponent에서 제외된다, 인자값 true를 넣으면 비활성화된 오브젝트도 GetComponent로 가져올 수 있다
    }
    
    void Update()
    {
        // if (!GameManager.instance.isLive) {
        //     return; // Update 계얼조직에 isLive가 false이면 동작하지 못하도록 조건 추가
        // }

        // inputVec.x = Input.GetAxisRaw("Horizontal"); 
        // // input = 유니티에서 받는 모든 입력을 관리하는 클래스
        // // Unity의 Inpuy Manager에서 Horizontal로 저장되있는 키가 눌렸는지를 확인 
        // // GetAxis가 아닌 GetAxisRaw로 더욱 명확한 컨트롤 구현 가능
        // inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() // 물리 연산 프레임마다 호출되는 생명주기 함수
    {
        if (!GameManager.instance.isLive) {
            return; // isLive가 false이면(시간이 멈추면) 동작하지 못하도록 조건 추가
        }

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        // normalized = 벡터값의 크기가 1이 되도록 좌표가 수정된 값, 이를 사용하지 않으면 대각선은 루트2를 이동
        // FixedUpdate안에서 실행되므로 DeltaTime이 아닌 fixedDeltaTime 사용

        // // 1. 힘을 준다
        // rigid.AddForce(inputVec);

        // // 2. 속도를 직접 제어한다
        // rigid.linearVelocity = inputVec; // velocity대신 linearVelocity사용권장

        // 3. 위치를 옮긴다 (현재 게임에서는 3번 사용)
        rigid.MovePosition(rigid.position + nextVec); // MovePosition은 속도가 아닌 위치이동이라 현재의 위치도 더해주어야한다.
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>(); 
        // value와 inputVec의 타입이 다르기 때문에 value.Get<Vector2>()를 써주어야한다
        // value의 Vector2값은 유니티에서 normalized되도록 설정했기 때문에 nextVec에는 normalized를 빼도 된다
    }

    void LateUpdate() // 프레임이 종료되기 전 실행되는 생명주기 함수
    {
        if (!GameManager.instance.isLive) {
            return; // isLive가 false이면(시간이 멈추면) 동작하지 못하도록 조건 추가
        }

        anim.SetFloat("Speed", inputVec.magnitude); 
        // 에니메이션의 Float값을 바꿈, SetFloat("바꿀 변수이름", 바꿀 크기)
        // inputVec.magnitude는 단순히 inputVec의 크기만을 입력받을때 사용
        
        if (inputVec.x != 0) { // inputVec의 x값이 변할때 실행 
            spriter.flipX =  inputVec.x < 0; // inputVec.x가 0보다 작을때 spriter.flipX가 참이 되어 반전되어야함
        }
    }

    void OnCollisionStay2D(Collision2D other) 
    {
        if (!GameManager.instance.isLive) {
            return;
        }

        GameManager.instance.health -= Time.deltaTime * 10;
        // Time.dletaTime을 활용하여 적절한 피격 데미지 계산, 그냥 -= 10을 하게 되면 프레임마다 체력이 10씩 닳기 때문에 빠르게 죽어버림
    
        if (GameManager.instance.health < 0) { 
            // Player는 자식오브젝트를 많이 가지고있는데 플레이어 사망시 이 오브젝트들을 비활성화 시켜줘야한다
            for (int index=2; index < transform.childCount; index++) { // childCout = 자식오브젝트의 개수
                transform.GetChild(index).gameObject.SetActive(false); // GetChild = 주어진 인덱스의 자식 오브젝트를 반환하는 함수 
                // GetChild의 반환값으로 transform이 나오므로 .gameObject로 다시 접근하여 SetActive를 false로 설정한다
            }

            anim.SetTrigger("Dead"); // 애니메이터 SetTrigger 함수로 죽음 애니메이션 실행
        }
    }
}
