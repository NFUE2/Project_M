using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : Character
{
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    //캐릭터의 기본 데이터 설정
    public override void initSetting()
    {
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
}
