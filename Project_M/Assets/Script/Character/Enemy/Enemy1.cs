using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Character
{
    public override void Attack(GameObject projectile, GameObject fire_pos)
    {

    }

    public override void initSetting(Animator animator)
    {
        data.hp = 1.0f;
        data.speed = 10.0f;
        data.damage = 1.0f;
        data.jumping = false;
    }

    public override void Move()
    {

    }
}
