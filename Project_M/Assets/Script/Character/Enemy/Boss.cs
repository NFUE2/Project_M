using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    bool move = false;

    public override void initSetting(Vector3 fire_pos, GameObject projectile)
    {
        data.user = User.Com;

        data.Player = GameObject.Find("Player");

        data.animator = gameObject.GetComponent<Animator>();

        data.hp = 10.0f;
        data.speed = 6.0f;
        data.damage = 1.0f;
        data.score = 5000;

        data.attack_distance = 5.0f;
        data.player_search = false;
    }

    public override void Monster_Action()
    {
        if (!data.player_search && Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < data.search_distance)
            StartCoroutine(Action_Stop());

        if(move)
        {
            data.animator.SetBool("Move", true);
            Vector3 dir = GameObject.Find("Player").transform.position - transform.position;
            dir.y = 0.0f;
            transform.rotation = dir.x > 0.0f ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 270, 0); //플레이어 위치에 따른 방향전환

            transform.position += dir.normalized * data.speed * Time.deltaTime;
            
            if(Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < data.attack_distance)
            {
                data.animator.SetBool("Move", false);
                move = false;
                StartCoroutine(Action_Stop());
            }
        }
    }


    IEnumerator Action_Stop()
    {
        if (!data.player_search && Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < data.search_distance)
        {
            data.animator.SetTrigger("PlayerSearch");
            data.player_search = true;

            yield return new WaitForSeconds(2.5f);
            StartCoroutine(Action_Stop());
        }

        else if (data.player_search)
        {
            Vector3 dir = GameObject.Find("Player").transform.position - transform.position;
            transform.rotation = dir.x > 0.0f ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 270, 0);

            if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > data.attack_distance)
                move = true;
            else
            {
                int random = Random.Range(0, 2);

                switch(random)
                {
                    case 0:
                        StartCoroutine(Attack1());
                        break;
                    case 1:
                        StartCoroutine(Attack2());
                        break;
                }
                yield return new WaitForSeconds(2.0f);
            }
        }
    }

    IEnumerator Attack1()
    {
        data.animator.SetTrigger("Attack1");
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(Action_Stop());
    }

    IEnumerator Attack2()
    {
        data.animator.SetTrigger("Attack2");

        yield return new WaitForSeconds(4.0f);
        StartCoroutine(Action_Stop());
    }
}
