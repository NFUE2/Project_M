using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�̱��� ���������� �ν��Ͻ�����
    public static GameManager instance; 

    public GameObject player; //�÷��̾� ���ӿ�����Ʈ�� ���� ����


    //���� Ȯ�� ����
    private bool player_survive = true; //�÷��̾� ���� ����
    private bool stage_clear = false; //�������� Ŭ���� ����
    private bool game_end = false; //������ ������ �������� ����
    private bool boss = false; //���������� ����

    private int score = 0; //���ھ�

    private void Awake()
    {
        instance = this; //�̱��� ����
        DontDestroyOnLoad(gameObject); //���� ���ӿ�����Ʈ�� ���� ����Ǿ �������� �ʰ���
    }

    private void Update()
    {
        //�׽�Ʈ�� ���� escŰ 
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
