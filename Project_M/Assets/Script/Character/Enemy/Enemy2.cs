using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��������Ʈ�� �̵��ϸ鼭 �ڽ��� ���±� �տ� ���� �ִ��� ������ üũ�ϴ� ��
public class Enemy2 : Character
{
    [Range(0.0f, 30.0f)] //ĳ���ͺ��� Ž�������� �޸��ϱ����� ��������
    public float search_distance; //���� Ž���Ÿ�

    Vector3 origin_Pos; //ĳ������ �ʱ���ġ
    Vector3 Dir = Vector3.left;

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

        data.attack_distance = 1.5f;
        data.player_search = false;

        origin_Pos = transform.position;
        transform.rotation = Quaternion.Euler(0, 270, 0);
    }

    public override void Monster_Action()
    {
        float distance = Vector3.Distance(data.Player.transform.position, transform.position);

        if (distance <= data.attack_distance && data.attack_delay < data.attack_timing)
        {
            base.Close_Range_Attack("Player");
            Debug.Log("����");
        }

        else
        {
            if (data.player_search)
                Move();
            else
                Rally_Point();

            transform.position += Dir.normalized * data.speed * Time.deltaTime;
        }
    }

    public override void Move()
    {
        Dir = data.Player.transform.position - transform.position;
        Dir.y = 0.0f;
    }

    private void Rally_Point()
    {
        Search();
        float distance = Vector3.Distance(transform.position, origin_Pos);
        Vector3 dir = transform.position - origin_Pos;

        if (distance >= 5.0f && dir.x < 0.0f)
        {
            Dir = Vector3.right;
            transform.rotation = Quaternion.Euler(0,0,0);
        }

        else if (distance >= 5.0f && dir.x > 0.0f)
        {
            Dir = Vector3.left;
            transform.rotation = Quaternion.Euler(0,180,0);
        }
    }

    private void Search()
    {
        Vector3 target_Pos = data.Player.transform.position - transform.right;

        float angle = Mathf.Atan2(target_Pos.x, target_Pos.y) * Mathf.Rad2Deg;
        float distance = Vector3.Distance(transform.position, data.Player.transform.position);

        if(distance <= search_distance && (50.0f <= angle && angle <= 100.0f))
            data.player_search = true;
    }
}
