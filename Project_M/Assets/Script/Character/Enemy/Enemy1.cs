using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ������ �÷��̾ �����ϸ� �����ؼ� �÷��̾ �����ϴ� �� ��ũ��Ʈ
public class Enemy1 : Character
{
    [Range(0.0f,30.0f)] //ĳ���ͺ��� Ž�������� �޸��ϱ����� ��������
    public float search_distance; //���� Ž���Ÿ�
    bool player_search = false; //�÷��̾ Ž������

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

        data.jumping = false;
    }

    public override void monsterAction()
    {
        //�÷��̾�� ĳ���� ������ �Ÿ�
        float distance = Vector3.Distance(data.Player.transform.position, transform.position); 

        if (distance <= search_distance) player_search = true; //Ž�� ���� �ȿ� ������ �÷��̾ �ν�

        //�÷��̾ �ν��ϰ� ���ݹ������� �� ��� �۵�
        if (player_search && distance > data.attack_distance)
            Move();
        //���ݹ��� �ȿ� ������ ���ݱ�ȸ�� ������ ����
        else if (distance <= data.attack_distance && data.attack_timing >= data.attack_delay)
            Attack(null, null);
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

    public override void Attack(GameObject projectile, GameObject fire_pos)
    {
        base.Attack(projectile,fire_pos);
        Debug.Log("����");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,search_distance);
    }

    public override void Damage()
    {
        foreach (Collider col in Physics.OverlapBox
            (transform.position + transform.right,
            new Vector3(0.5f, 0.5f, 0f),
            Quaternion.Euler(0, 0, 0),
            LayerMask.GetMask("Player"))) //int�� ����Ϸ������� �۵��� ������,Enemy�� ���̾�� 12��
        {
            //�ش� ��ü�� ��ũ��Ʈ�� �����Ͽ� ������(ü�°���)
            col.GetComponent<Character_Controller>().character.data.hp -= data.damage;
            Debug.Log("�÷��̾� ���ݼ���");
        }
    }
}
