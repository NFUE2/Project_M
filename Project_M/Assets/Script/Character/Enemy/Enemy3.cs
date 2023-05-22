using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Character
{
    public override void initSetting(Vector3 fire_pos, GameObject projectile)
    {
        data.user = User.Com;

        data.Player = GameObject.Find("character");

        data.animator = gameObject.GetComponent<Animator>();

        data.hp = 1.0f;
        data.speed = 5.0f;

        data.attack_delay = 3.0f;
        data.attack_timing = 3.0f;

        data.fire_pos = fire_pos;
        data.projectile = projectile;

        data.player_search = false;

        data.attack_distance = 20f;

        //오브젝트 풀을 이용하기위한 조건문
        if(fire_pos !=null && projectile != null)
        {
            data.open_Obj = new List<GameObject>();
            data.close_Obj = new List<GameObject>();

            data.open_Obj = Create_projectile(data.projectile,transform);
        }
    }
    
    public override void Long_Range_Attack()
    {
        data.open_Obj[0].SetActive(true);
        data.close_Obj.Add(data.open_Obj[0]);
        data.open_Obj.Remove(data.open_Obj[0]);
    }

    public override void Monster_Action()
    {
        float distance = Vector3.Distance(transform.position, data.Player.transform.position);

        if(distance <= data.attack_distance)
        {
            Long_Range_Attack();

            if (!data.player_search) data.player_search = true;
        }
        else if(data.player_search && data.attack_distance * 0.75 <= distance)
            Move();
    }

    public override void Move()
    {
        Vector3 dir = data.Player.transform.position - transform.position;

        transform.position += dir.normalized * data.speed * Time.deltaTime;
    }

    //원거리 공격을 하는 캐릭터는 Attack_Event함수를 사용하지 않음

}
