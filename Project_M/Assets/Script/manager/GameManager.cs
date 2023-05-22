using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //싱글톤 패턴을위한 인스턴스변수
    public static GameManager instance;
    private int score;

    private void Awake()
    {
        instance = this; //싱글톤 생성
        DontDestroyOnLoad(gameObject); //현재 게임오브젝트를 씬이 변경되어도 삭제되지 않게함
    }

    public int P_score { get { return score; } set { score += value; } }
}
