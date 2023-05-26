using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //싱글톤 패턴을위한 인스턴스변수
    public static GameManager instance; 

    public GameObject player; //플레이어 게임오브젝트를 담을 변수


    //각종 확인 변수
    private bool player_survive = true; //플레이어 생존 유무
    private bool stage_clear = false; //스테이지 클리어 유무
    private bool game_end = false; //게임이 완전히 끝났는지 유무
    private bool boss = false; //보스전인지 유무

    private int score = 0; //스코어

    private void Awake()
    {
        instance = this; //싱글톤 생성
        DontDestroyOnLoad(gameObject); //현재 게임오브젝트를 씬이 변경되어도 삭제되지 않게함
    }

    private void Update()
    {
        //테스트를 위한 esc키 
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1.0f)
            Time.timeScale = 0.0f;
        else if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0.0f)
            Time.timeScale = 1.0f;
    }

    public int P_score { get { return score; } set { score += value; } }

    public GameObject P_player { get { return player; } set { player = value; } }

    public bool P_player_survive { get { return player_survive; } set { player_survive = value; } }

    public bool P_stage_clear { get { return stage_clear; } set { stage_clear = value; } }

    public bool P_game_end { get { return game_end; } set { game_end = value; } }
    public bool P_boss { get { return boss; } set { boss = value; } }

}
