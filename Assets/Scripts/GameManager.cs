using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 스크립트들을 짤때 Player를 직접 public으로 받아도 되지만 깔끔한 코딩을 위해 GameManager에서 Player를 받아 static으로 선언하여 사용한다
    public static GameManager instance; // static = 정적으로 사용하겠다는 키워드. 바로 메모리에 얹어버림
    // static으로 선언된 변수는 인스켓터에 나타나지 않는다
    
    public float gameTime; // 게임시간
    public float maxGametime = 2 * 10f; // 최대게임시간
    
    public Player player;
    public PoolManager pool; // 다양한 곳에서 쉽게 접근할 수 있도록 게임매니저에 풀 매니저 추가

    void Awake() 
    {
        instance = this;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGametime) {
            gameTime = maxGametime;
        }
    }
}
