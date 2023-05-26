using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Stage_Manager : Manager
{
    //적들의 탐지범위를 설정합니다.
    [Range(0.0f, 30.0f)]
    public float[] distance;

    public GameObject[] create_unit; //생성될 적 게임오브젝트를 입력받습니다.
    public GameObject[] create_pos;  //생성될 적 게임오브젝트의 위치를 입력 받습니다.

    public BoxCollider boxCollider; //보스전에 사용할 벽을 입력받습니다.

    public AudioClip clear_voice; //보이스 음악을 입력받습니다.
    AudioSource audio; //음향을 관리할 AudioSource입니다.
    bool voicechk = false; //음향이 작동했는지 안했는지 확인하는 변수입니다.

    GameObject score; //스코어를 보여주는 변수입니다.
    public GameObject Info_txt; //스테이지 안내변수입니다.


    public override void Manager_Start()
    {
        audio = GetComponent<AudioSource>(); //AudioSource할당

        Info_txt.GetComponent<Animator>().enabled = true; //애니메이션 작동

        score = GameObject.Find("Score"); //게임오브젝트 할당

        GameObject player = Instantiate(GameManager.instance.P_player); //플레이어 캐릭터 복사
        player.transform.rotation = Quaternion.Euler(0, 90, 0); //플레이어 캐릭터 회전값 입력
        player.transform.position = new Vector3(0, 2, 0); //플레이어 크기 설정
        player.name = "Player"; //플레이어 이름 설정

        for (int i = 0; i < create_pos.Length; i++) //생성할 적의 수만큼 반복합니다
        {
            GameObject enemy = Instantiate(create_unit[i]); //적을 복사

            enemy.GetComponent<Character>().data.search_distance = distance[i]; //탐지범위를 입력

            if (i == create_pos.Length - 1) //마지막 적은 보스로 설정합니다.
                enemy.gameObject.name = "Boss";
            
            //생성될 적의 회전,크기,위치값 입력
            enemy.transform.rotation = create_pos[i].transform.rotation;
            enemy.transform.localScale = new Vector3(2, 2, 2);
            enemy.transform.position = create_pos[i].transform.position;
        }
    }

    public override void Manager_Update()
    {
        score.GetComponent<TextMeshProUGUI>().text = GameManager.instance.P_score.ToString(); //스코어를 입력

        if(GameObject.Find("Player").GetComponent<Character>().data.hp <= 0.0f || GameObject.Find("Boss").GetComponent<Character>().data.hp <= 0.0f) //만약 플레이어가 사망하거나 보스가 사망할경우 작동
        {
            if(GameObject.Find("Player").GetComponent<Character>().data.hp <= 0.0f) //플레이어가 사망하면 게임오버 텍스트 입력
                Info_txt.GetComponent<TextMeshProUGUI>().text = "GAME OVER";

            else if (GameObject.Find("Boss").GetComponent<Character>().data.hp <= 0.0f) //보스가 사망하면 미션성공 텍스트 및 음성 출력
            {
                VoicePlay();
                Info_txt.GetComponent<TextMeshProUGUI>().text = "MISSON COMPLETE";
            }

            Info_txt.GetComponent<Animator>().SetTrigger("Info"); //안내 텍스트 애니메이션 시작
        }

        if (Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Boss").transform.position) < 25.0f) //보스방 진입시 작동
        {
            boxCollider.enabled = true;  //플레이어가 벗어나지못하게 벽 생성
            if(!GameManager.instance.P_stage_clear) //클리어하지못했다면 보스전 시작
                GameManager.instance.P_boss = true;
        }
    }

    void VoicePlay() //음성 시작
    {
        if (voicechk) return;
        audio.clip = clear_voice;
        audio.Play();
        voicechk = true;
    }
}