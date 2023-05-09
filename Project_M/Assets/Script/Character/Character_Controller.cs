using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public Character character;
    public GameObject fire_Pos;
    public GameObject projectile;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>(); //각 캐릭터의 애니메이션
        character.initSetting(animator); //각 캐릭터의 초기변수 세팅
    }

    private void Update()
    {
        //체력이 0이하로 떨어지면 사망하게함
        if (character.data.hp < 0.0f)
        {
            //animator.SetTrigger("Dead");
            gameObject.GetComponent<Character_Controller>().enabled = false;
            return;
        }
    }

    private void FixedUpdate()
    {
        //해당 스크립트가 받아온 character 변수가 플레이어인지 컴퓨터인지 구분
        if (character.data.user == User.Player)
        {
            //해당클래스에서 제작한 이동함수 작동
            character.Move();

            //A키를 누르면 공격을함
             if (Input.GetAxis("Attack") == 1.0f)
                character.Attack(projectile, fire_Pos);
        }
        else
            character.mosterAction();

    }
}
