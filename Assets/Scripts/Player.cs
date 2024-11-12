using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed; 
    public Scanner scanner; // 플레이어 스크립트에서 Scanner클래스 타입 변수 선언 및 초기화

    Rigidbody2D rigid;
    SpriteRenderer spriter; // SpriteRenderer 값을 받아올 변수
    Animator anim; 

    void Awake ()
    {
        rigid = GetComponent<Rigidbody2D>(); // GetComponent<컴포넌트 이름> = 오브젝트에서 컴포넌트를 가져오는 함수
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();

    }
    
    void Update()
    {
    //     inputVec.x = Input.GetAxisRaw("Horizontal"); 
    //     // input = 유니티에서 받는 모든 입력을 관리하는 클래스
    //     // Unity의 Inpuy Manager에서 Horizontal로 저장되있는 키가 눌렸는지를 확인 
    //     // GetAxis가 아닌 GetAxisRaw로 더욱 명확한 컨트롤 구현 가능
    //     inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() // 물리 연산 프레임마다 호출되는 생명주기 함수
    {
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
        anim.SetFloat("Speed", inputVec.magnitude); 
        // 에니메이션의 Float값을 바꿈, SetFloat("바꿀 변수이름", 바꿀 크기)
        // inputVec.magnitude는 단순히 inputVec의 크기만을 입력받을때 사용
        
        if (inputVec.x != 0) { // inputVec의 x값이 변할때 실행 
            spriter.flipX =  inputVec.x < 0; // inputVec.x가 0보다 작을때 spriter.flipX가 참이 되어 반전되어야함
        }
    }
}
