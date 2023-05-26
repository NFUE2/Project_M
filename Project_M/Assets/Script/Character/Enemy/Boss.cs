using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    bool move = false; //이동중인지 아닌지 확인하는 변수

    //캐릭터 기본세팅
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

    //반복문에서 작동할 함수
    public override void Monster_Action()
    {
        //캐릭터가 일정범위 이내로 들어오면 작동
        if (!data.player_search && Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < data.search_distance)
            StartCoroutine(Action_Stop());

        //이동해야할경우 작동
        if(move)
        {
            data.animator.SetBool("Move", true); //애니메이션 작동
            Vector3 dir = GameObject.Find("Player").transform.position - transform.position; //방향 설정
            dir.y = 0.0f; //y값은 항상 0
            transform.rotation = dir.x > 0.0f ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 270, 0); //플레이어 위치에 따른 방향전환

            transform.position += dir.normalized * data.speed * Time.deltaTime; //플레이어의 방향으로 이동
            
            //공격범위안에 들어오면 이동을 멈추고 공격
            if(Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < data.attack_distance)
            {
                data.animator.SetBool("Move", false);
                move = false;
                StartCoroutine(Action_Stop());
            }
        }
    }

    //공격하는 코루틴
    IEnumerator Action_Stop()
    {
        //플레이어 인식못한채,범위안에 캐릭터가 들어올경우,조우시 작동
        if (!data.player_search && Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < data.search_distance)
        {
            data.animator.SetTrigger("PlayerSearch");
            data.player_search = true;

            yield return new WaitForSeconds(2.5f);
            StartCoroutine(Action_Stop());
        }

        //플레이어를 인식했을경우
        else if (data.player_search)
        {
            Vector3 dir = GameObject.Find("Player").transform.position - transform.position; //캐릭터의 방향 확인
            transform.rotation = dir.x > 0.0f ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 270, 0); //플레이어 위치에 따른 방향전환

            //캐릭터가 멀경우 작동
            if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > data.attack_distance)
                move = true;
            //아닐경우 공격패턴 작동
            else
            {
                int random = Random.Range(0, 2); //랜덤으로 공격패턴이 정해짐

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

    //해당 애니메이션을 작동시켜서 공격을하게됨
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
