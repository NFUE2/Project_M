using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    public override void initSetting(Vector3 fire_pos, GameObject projectile)
    {
        data.user = User.Com;

        data.Player = GameObject.Find("character");

        data.animator = gameObject.GetComponent<Animator>();

        data.hp = 1.0f;
        data.speed = 6.0f;
        data.damage = 1.0f;

        data.attack_delay = 3.0f;
        data.attack_timing = 3.0f;

        data.attack_distance = 1.5f;
    }

    public override void Long_Range_Attack()
    {

    }


    public override void Monster_Action()
    {

    }

    public override void Move()
    {

    }
}
