using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    public override void initSetting(Vector3 fire_pos, GameObject projectile)
    {
        data.user = User.Com;

        data.Player = GameObject.Find("Player");

        data.animator = gameObject.GetComponent<Animator>();

        data.hp = 5.0f;
        data.speed = 6.0f;
        data.damage = 1.0f;

        data.attack_delay = 3.0f;
        data.attack_timing = 1.5f;
        data.player_search = false;

        data.attack_distance = 3.0f;
    }

    public override void Monster_Action()
    {

    }

    public override void Move()
    {

    }
}
