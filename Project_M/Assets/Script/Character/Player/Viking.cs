using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : Character
{
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    //ĳ������ �⺻ ������ ����
    public override void initSetting()
    {
        data.hp = 1.0f;
        data.speed = 10.0f;
        data.damage = 1.0f;
        data.jumping = false;
    }

    public override void Move()
    {
        //�θ��� Move�Լ��� �۵�
        base.Move();
    }
}
