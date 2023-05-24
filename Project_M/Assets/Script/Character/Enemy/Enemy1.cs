using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ������ �÷��̾ �����ϸ� �����ؼ� �÷��̾ �����ϴ� �� ��ũ��Ʈ
public class Enemy1 : Character
{
    [Range(0.0f,30.0f)] //ĳ���ͺ��� Ž�������� �޸��ϱ����� ��������
    public float search_distance; //���� Ž���Ÿ�

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
        //�÷��̾�� ĳ���� ������ �Ÿ�
        float distance = Vector3.Distance(data.Player.transform.position, transform.position); 

        if (distance <= search_distance) data.player_search = true; //Ž�� ���� �ȿ� ������ �÷��̾ �ν�

        //�÷��̾ �ν��ϰ� ���ݹ������� �� ��� �۵�
        if (data.player_search && distance > data.attack_distance)
            Move();
        //���ݹ��� �ȿ� ������ ���ݱ�ȸ�� ������ ����
        else if (distance <= data.attack_distance && data.attack_timing >= data.attack_delay)
        {

        }
            //base.Close_Range_Attack("Player");
    }

    //���Ͱ� ĳ������ �������� �̵�
    public override void Move()
    {
        //ĳ������ �������� �̵�
        Vector3 dir = data.Player.transform.position - transform.position;
        dir.y = 0.0f;

        transform.rotation = dir.x > 0.0f ? Quaternion.Euler(0,0,0) : Quaternion.Euler(0, 180, 0); //�÷��̾� ��ġ�� ���� ������ȯ
        transform.position += dir.normalized * data.speed * Time.deltaTime; //�÷��̾� �������� �̵�
    }

    public override void Long_Range_Attack()
    {
        base.Long_Range_Attack();
    }
}
