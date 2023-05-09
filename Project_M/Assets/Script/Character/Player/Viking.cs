using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : Character
{
    public override void Attack(GameObject projectile, GameObject fire_pos)
    {
        base.Attack(projectile, fire_pos);
    }

    //캐릭터의 기본 데이터 설정
    public override void initSetting(Animator animator)
    {
        data.user = User.Player;
        data.animator = animator;
        data.hp = 1.0f;
        data.speed = 10.0f;
        data.damage = 1.0f;
        data.jumping = false;
    }

    public override void Move()
    {
        //부모의 Move함수를 작동
        base.Move();
    }

    //애니메이션 이벤트를 이용하여 적에게 데미지를 줌
    public void Damage()
    {
        //해당 범위내의 모든 적들에게 데미지를 입힘
        foreach (Collider col in Physics.OverlapBox
            (transform.position + transform.right,
            new Vector3(0.5f, 0.5f, 0f),
            Quaternion.Euler(0, 0, 0),
            LayerMask.GetMask("Enemy"))) //int로 사용하려햇으나 작동을 안했음,Enemy의 레이어는 12번
        {
            //해당 개체의 스크립트를 참조하여 데미지(체력감소)
            col.GetComponent<Character_Controller>().character.data.hp -= data.damage;
        }
    }
}
