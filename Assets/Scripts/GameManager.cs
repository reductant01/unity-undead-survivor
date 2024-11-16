using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 스크립트들을 짤때 Player를 직접 public으로 받아도 되지만 깔끔한 코딩을 위해 GameManager에서 Player를 받아 static으로 선언하여 사용한다
    public static GameManager instance; // static = 정적으로 사용하겠다는 키워드. 바로 메모리에 얹어버림
    // static으로 선언된 변수는 인스켓터에 나타나지 않는다
    
    [Header("# Game Control")] // Header = 인스펙터의 속성들을 이쁘게 구분시켜주는 타이틀
    public float gameTime; // 게임시간
    public float maxGametime = 2 * 10f; // 최대게임시간
    
    [Header("# Player Info")]
    public bool isLive; // 시간 정지 여부를 알려주는 bool 변수 선언
    public float health; // 생명력 관련 변수는 float로 설정
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = {3, 5, 10, 100, 150, 210, 280, 360, 450, 600}; // 각 레벨의 필요경험치를 보관한 배열변수 선언 및 초기화

    [Header("# GameObject")]
    public Player player;
    public PoolManager pool; // 다양한 곳에서 쉽게 접근할 수 있도록 게임매니저에 풀 매니저 추가
    public LevelUp UILevelUp;

    void Awake() 
    {
        instance = this;
    }

    public void GameStart() // 유니티 기능을 사용하여 Button Start의 On Click과 함수를 연결
    {
        health = maxHealth; // 시작할 때 현재 체력과 최대 체력이 같도록 로직 추가

        // 임시 스크립트 (첫번째 캐릭터 선택)
        UILevelUp.Select(0); // 게임이 처음 시작하면 기본 근접무기 하나를 지급
        isLive = true;
    }

    void Update()
    {
        if (!isLive) {
            return; // Update 계얼조직에 isLive가 false이면 동작하지 못하도록 조건 추가
        }

        gameTime += Time.deltaTime;

        if (gameTime > maxGametime) {
            gameTime = maxGametime;
        }
    }

    public void GetExp()
    {
        exp++;

        // if 조건으로 필요 경험치에 도달하면 레벨 업 하도록 작성
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) { // 무한레벨 구현을 위해 Min 함수를 사용하여 최고 경험치를 그대로 사용하도록 변경
            level++;
            exp = 0;
            UILevelUp.Show(); // 게임매니저의 레벨업 로직에 창을 보여주는 함수 호출
        }
    }

    public void Stop() // 시간정지, 레벨업시 호출
    {
        isLive = false;
        Time.timeScale = 0; // timeScale = 유니티의 시간 속도 (배율), 초기값은 1
    }

    public void Resume() // 이후 아이템 선택시 다시 시간이 작동
    {
        isLive = true;
        Time.timeScale = 1; // 만약 Time.timeScale = 2라면 2배속
    }
}
