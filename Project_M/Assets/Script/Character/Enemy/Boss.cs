using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    public override void initSetting()
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

    public override void Attack(GameObject projectile, GameObject fire_pos)
    {
    }

    public override void Damage(string layer)
    {
    }

    public override void monsterAction()
    {
    }

    public override void Move()
    {
    }
}
