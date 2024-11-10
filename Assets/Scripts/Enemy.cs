using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon; // 몬스터의 애니매이션을 받을 변수
    public Rigidbody2D target;

    bool isLive; // 살아있는지를 확인

    Rigidbody2D rigid; // 위치이동을 위해 rigid 변수 생성
    Animator anim; // 받은 애니매이션들을 저장할 변수
    SpriteRenderer spriter; // 방향 반전을 위해 SpriteRenderer 변수 생성
    
    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive) // 살아있지 않다면 아래의 코드를 실행하지 않는다
            return;

        Vector2 dirVec = target.position - rigid.position; 
        // 위치차이 = 타겟 위치 - 나의 위치
        // 방향 = 위치차이의 정규화 (Normalized)
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec); // 플레이어의 키입력 값을 더한 이동 + 몬스터의 방향값을 더한 이동
        rigid.linearVelocity = Vector2.zero; // 플레이어와 충돌했을때 rigid의 물리속도가 이동에 영향을 주지 않도록 속도를 제거
    }

    void LateUpdate()
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x; 
        // 목표의 X축 값과 자신의 X축값을 비교하여 작으면 true가 되도록 설계
        // 작다 = 몬스터가 왼쪽으로 이동해야한다
    }

    // Enemy의 target을 설정해야하지만 prefab은 장면의 오브젝트를 접근할수 없다
    // Enemy가 생성될 때 target을 초기화 해줄 필요가 있다
    void OnEnable() // OnEnable = 스크립트가 활성화 될때 호출되는 이벤트 함수, 
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>(); // OnEnable에서 타겟변수에 게임매니저를 활용하여 플레이어 할당, target은 RigidBody2D이므로 GetComponent가 필요하다
        isLive = true; // 처음 생성될때는 살아있도록 설정
        health = maxHealth; // 처음 생성될때는 최대체력으로 생성되도록 해야한다   
    }

    public void Init(SpawnData data) // 레벨링에 따른 몬스터 상태 변경
    {
        anim.runtimeAnimatorController = animCon[data.spriteType]; // 매개변수의 속성을 몬스터 속성변경에 활용하기
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
}
