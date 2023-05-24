using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : Character
{
    //ĳ������ �⺻ ������ ����
    public override void initSetting(Vector3 fire_pos, GameObject projectile)
    {
        data.user = User.Player;

        data.animator = gameObject.GetComponent<Animator>();

        data.hp = 1.0f;
        data.speed = 10.0f;
        data.damage = 1.0f;

        data.attack_delay = 1.0f;
        data.attack_timing = 1.0f;
        //data.charging = 0.0f;

        data.jumping = false;
    }

    public override void Move()
    {
        //�θ��� Move�Լ��� �۵�
        base.Move();
    }

    public override void Long_Range_Attack()
    {
        Close_Range_Attack();
    }

}
