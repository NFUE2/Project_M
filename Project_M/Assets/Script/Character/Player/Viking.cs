using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : Character
{
    public override void Attack(GameObject projectile, GameObject fire_pos)
    {
        //Debug.Log("공격");
        //base.Attack(projectile, fire_pos);
    }

    //캐릭터의 기본 데이터 설정
    public override void initSetting()
    {
        data.user = User.Player;

        data.animator = gameObject.GetComponent<Animator>();

        data.hp = 1.0f;
        data.speed = 10.0f;
        data.damage = 1.0f;

        data.charging = 0.0f;

        data.jumping = false;
    }

    public override void Move()
    {
        //부모의 Move함수를 작동
        base.Move();
    }

    //애니메이션 이벤트를 이용하여 적에게 데미지를 줌
    public override void Damage(string layer)
    {
        base.Damage("Enemy");
    }
}
