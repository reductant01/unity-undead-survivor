using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 inputVec;

    Rigidbody2D rigid;

    void Awake ()
    {
        rigid = GetComponent<Rigidbody2D>(); // GetComponent<컴포넌트 이름> = 오브젝트에서 컴포넌트를 가져오는 함수
    }
    
    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxis("Horizontal"); 
        // input = 유니티에서 받는 모든 입력을 관리하는 클래스
        // Unity의 Inpuy Manager에서 Horizontal로 저장되있는 키가 눌렸는지를 확인 
        inputVec.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate() // 물리 연산 프레임마다 호출되는 생명주기 함수
    {
        Vector2 nextVec = inputVec.normalized;
        // // 1. 힘을 준다
        // rigid.AddForce(inputVec);

        // // 2. 속도를 직접 제어한다
        // rigid.linearVelocity = inputVec; // velocity대신 linearVelocity사용권장

        // 3. 위치를 옮긴다 (현재 게임에서는 3번 사용)
        rigid.MovePosition(rigid.position + inputVec); // MovePosition은 속도가 아닌 위치이동이라 현재의 위치도 더해주어야한다.
    }
}
