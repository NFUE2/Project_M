using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public Character character; //어떤 캐릭터를 받아올것인지에 대한 클래스변수

    public GameObject fire_Pos; //원거리 캐릭터의 경우 투사체가 발사될 위치
    public GameObject projectile; //원거리 캐릭터의 투사체 종류

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

        //체력이 0이하로 떨어지면 사망하게함
        if (character.data.hp <= 0.0f)
        {
            character.data.animator.SetTrigger("Dead"); //애니메이션에서 사망처리

            if (gameObject.layer == 13) //플레이어가 사망할경우
                GameManager.instance.P_player_survive = false;

            else if (gameObject.layer == 12) //적이 사망할경우
            {
                GameManager.instance.P_score = character.data.score;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
            gameObject.GetComponent<Character_Controller>().enabled = false;

            //스크립트가 더이상 작동하지 않도록 처리
            return;
        }
    }

    private void FixedUpdate()
    {
        //해당 스크립트가 받아온 character 변수가 플레이어인지 컴퓨터인지 구분
        if (character.data.user == User.Player) //플레이어의 경우 작동
        {
            //해당클래스에서 제작한 이동함수 작동
            character.Move();

            //A키를 누르면 공격을함
            if (Input.GetAxis("Attack") == 1.0f && character.data.attack_delay < character.data.attack_timing)
            {
                character.Attack();
            }

            ////A키를 누르고 있으면 차징공격을 준비
            //else if (Input.GetButton("Attack"))
            //    character.data.charging += Time.deltaTime;

            ////차징이 어느정도 되고 키를 떼면 차징공격이 나감
            //else if(Input.GetButtonUp("Attack"))
            //{
            //    character.Charging_Attack(projectile, fire_Pos);
            //    character.data.charging = 0.0f;
            //}
        }
        else //몬스터의 경우 작동
            character.Monster_Action();
    }
}
