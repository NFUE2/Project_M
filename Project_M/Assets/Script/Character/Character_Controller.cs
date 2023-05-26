using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//전략패턴으로 캐릭터를 사용하기위한 클래스
public class Character_Controller : MonoBehaviour
{
    public Character character; //어떤 캐릭터를 받아올것인지에 대한 클래스변수

    public GameObject fire_Pos; //원거리 캐릭터의 경우 투사체가 발사될 위치
    public GameObject projectile; //원거리 캐릭터의 투사체 종류

    public AudioClip dead_sound;

    private void Start()
    {
        if (fire_Pos == null && projectile == null)
            character.initSetting(Vector3.zero, null);
        else
            character.initSetting(fire_Pos.transform.position, projectile); //각 캐릭터의 초기변수 세팅
    }

    private void Update()
    {
        //시간이 지나면 공격기회를 얻음
        character.data.attack_timing += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //체력이 0이하로 떨어지면 사망하게함
        if (character.data.hp <= 0.0f)
        {
            GetComponent<AudioSource>().clip = dead_sound;
            GetComponent<AudioSource>().Play();

            character.data.animator.SetTrigger("Dead"); //애니메이션에서 사망처리
            if (gameObject.layer == 13) //플레이어가 사망할경우
            {
                GameManager.instance.P_player_survive = false;
                if (GameManager.instance.P_boss) GameManager.instance.P_boss = false;
            }

            else if (gameObject.layer == 12) //적이 사망할경우
            {
                GameManager.instance.P_score = character.data.score;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;

                if (gameObject.name == "Boss")
                {
                    GameManager.instance.P_stage_clear = true;
                    GameManager.instance.P_boss = false;
                }
            }
            gameObject.GetComponent<Character>().enabled = false;
            gameObject.GetComponent<Character_Controller>().enabled = false;

            //스크립트가 더이상 작동하지 않도록 처리
            return;
        }

        //해당 스크립트가 받아온 character 변수가 플레이어인지 컴퓨터인지 구분
        if (character.data.user == User.Player) //플레이어의 경우 작동
        {
            //해당클래스에서 제작한 이동함수 작동
            character.Move();

            //A키를 누르면 공격을함
            if (Input.GetAxis("Attack") == 1.0f && character.data.attack_delay < character.data.attack_timing)
                character.Attack();
        }
        else //몬스터의 경우 작동
            character.Monster_Action();
    }
}
