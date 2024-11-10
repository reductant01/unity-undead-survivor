using Mono.Cecil;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint; // 자식 오브젝트의 트랜스폼을 담을 배열 변수 선언 
    public SpawnData[] spawnData; // 만든 클래스를 그대로 타입으로 활용하여 배열 변수 선언

    int level;
    float timer; // 소환 타이머를 위한 변수 선언

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); // 자식 오브젝트들의 위치를 모두 가져옴, GetComponents인것 확인
    }

    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1); // 적절한 숫자로 나누에 시간에 맞춰 레벨이 올라가도록 작성
        // FloorToInt = 소수점 아래는 버리고 int형으로 바꾸는 함수 
        // CeilToInt = 소수점 아래를 올리고 int형으로 바꾸는 함수
        // Mathf.Min = 둘중 최솟값을 반환, 시간이 지나면서 level이 배열보다 커지는 에러가 발생하는데 이를 막기 위해 사용

        if (timer > spawnData[level].spawnTime) { // 타이머가 일정 시간 값에 도달하면 소환하도록 작성, 레벨을 활용해 소환타이밍을 변경
            timer = 0;
            Spawn();
        }  
    }

    void Spawn()  
    {
        GameObject enemy = GameManager.instance.pool.Get(0); // 게임매니저의 인스턴스까지 접근하여 풀링의 함수 호출, Get(1) = 배열에서 1번쨰 요소에 해당(좀비 = 0, 스켈레톤 = 1) 
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // Instantiate 반환 값을 변수에 넣고 이를 만들어둔 소환 위치 중 하나로 배치되도록 작성
        // GetComponentsInChildren는 자기 자신도 포함하여 가져오기때문에 Random.Range[]를 0부터 시작하면 미리 만들어둔 포인트가 아닌 자기 자신의 위치도 가져오게 된다
        enemy.GetComponent<Enemy>().Init(spawnData[level]); // 오브젝트 풀에서 가져온 오브젝트에서ㅓ Enemy 컴포넌트로 접근
    }
}

// 하나의 스크립트 내에 여러 클래스를 선언할 수 있다.
// 직렬화 (Serialization) = 개체를 저장 혹은 전송하기 위해 변환
[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}