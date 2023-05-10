using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public Character character; //어떤 캐릭터를 받아올것인지에 대한 클래스변수
    public GameObject fire_Pos; //원거리 캐릭터의 경우 투사체가 발사될 위치
    public GameObject projectile; //원거리 캐릭터의 투사체 종류

    Animator animator; //애니메이션

    private void Start()
    {
        animator = GetComponent<Animator>(); //각 캐릭터의 애니메이션
        character.initSetting(); //각 캐릭터의 초기변수 세팅
    }

    private void Update()
    {
        //시간이 지나면 공격기회를 얻음
        character.data.attack_timing += Time.deltaTime;

        //체력이 0이하로 떨어지면 사망하게함
        if (character.data.hp < 0.0f)
        {
            //animator.SetTrigger("Dead"); //애니메이션에서 사망처리

            //스크립트가 더이상 작동하지 않도록 처리
            gameObject.GetComponent<Character_Controller>().enabled = false;
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
            if (Input.GetButtonDown("Attack"))
                character.Attack(projectile, fire_Pos);
            else if (Input.GetButton("Attack"))
                character.data.charging += Time.deltaTime;

            else if(Input.GetButtonUp("Attack"))
            {
                character.Charging_Attack(projectile, fire_Pos);
                character.data.charging = 0.0f;
            }
        }
        else //몬스터의 경우 작동
            character.monsterAction();
    }
}
