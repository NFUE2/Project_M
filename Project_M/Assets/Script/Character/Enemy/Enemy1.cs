using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일정 범위에 플레이어가 접근하면 추적해서 플레이어를 공격하는 적 스크립트
public class Enemy1 : Character
{
    [Range(0.0f,30.0f)] //캐릭터별로 탐지범위를 달리하기위해 범위설정
    public float search_distance; //몬스터 탐지거리

    public override void initSetting(Vector3 fire_pos, GameObject projectile)
    {
        data.user = User.Com;

        data.Player = GameObject.Find("Player");

        data.animator = gameObject.GetComponent<Animator>();

        data.hp = 1.0f;
        data.speed = 6.0f;
        data.damage = 1.0f;

        data.attack_delay = 3.0f;
        data.attack_timing = 3.0f;
        data.player_search = false;

        data.attack_distance = 1.5f;
    }

    public override void Monster_Action()
    {
        //플레이어와 캐릭터 사이의 거리
        float distance = Vector3.Distance(data.Player.transform.position, transform.position); 

        if (distance <= search_distance) data.player_search = true; //탐지 범위 안에 들어오면 플레이어를 인식

        //플레이어를 인식하고 공격범위보다 멀 경우 작동
        if (data.player_search && distance > data.attack_distance)
            Move();
        //공격범위 안에 들어오고 공격기회가 있을때 공격
        else if (distance <= data.attack_distance && data.attack_timing >= data.attack_delay)
        {

        }
            //base.Close_Range_Attack("Player");
    }

    //몬스터가 캐릭터의 방향으로 이동
    public override void Move()
    {
        //캐릭터의 방향으로 이동
        Vector3 dir = data.Player.transform.position - transform.position;
        dir.y = 0.0f;

        transform.rotation = dir.x > 0.0f ? Quaternion.Euler(0,0,0) : Quaternion.Euler(0, 180, 0); //플레이어 위치에 따른 방향전환
        transform.position += dir.normalized * data.speed * Time.deltaTime; //플레이어 방향으로 이동
    }

    public override void Long_Range_Attack()
    {
        base.Long_Range_Attack();
    }
}
