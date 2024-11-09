using Mono.Cecil;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint; // 자식 오브젝트의 트랜스폼을 담을 배열 변수 선언 
    
    float timer; // 소환 타이머를 위한 변수 선언

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); // 자식 오브젝트들의 위치를 모두 가져옴, GetComponents인것 확인
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f) { // 타이머가 일정 시간 값에 도달하면 소환하도록 작성
            timer = 0;
            Spawn();
        }  
    }

    void Spawn()  
    {
        GameObject enemy = GameManager.instance.pool.Get(Random.Range(0, 2)); // 게임매니저의 인스턴스까지 접근하여 풀링의 함수 호출, Get(1) = 배열에서 1번쨰 요소에 해당(좀비 = 0, 스켈레톤 = 1) 
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // Instantiate 반환 값을 변수에 넣고 이를 만들어둔 소환 위치 중 하나로 배치되도록 작성
        // GetComponentsInChildren는 자기 자신도 포함하여 가져오기때문에 Random.Range[]를 0부터 시작하면 미리 만들어둔 포인트가 아닌 자기 자신의 위치도 가져오게 된다
    }
}
