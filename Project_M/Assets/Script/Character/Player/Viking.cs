using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : Character
{
    public override void Attack(GameObject projectile, GameObject fire_pos)
    {
        base.Attack(projectile, fire_pos);
    }

    //ĳ������ �⺻ ������ ����
    public override void initSetting(Animator animator)
    {
        data.user = User.Player;
        data.animator = animator;
        data.hp = 1.0f;
        data.speed = 10.0f;
        data.damage = 1.0f;
        data.jumping = false;
    }

    public override void Move()
    {
        //�θ��� Move�Լ��� �۵�
        base.Move();
    }

    //�ִϸ��̼� �̺�Ʈ�� �̿��Ͽ� ������ �������� ��
    public void Damage()
    {
        //�ش� �������� ��� ���鿡�� �������� ����
        foreach (Collider col in Physics.OverlapBox
            (transform.position + transform.right,
            new Vector3(0.5f, 0.5f, 0f),
            Quaternion.Euler(0, 0, 0),
            LayerMask.GetMask("Enemy"))) //int�� ����Ϸ������� �۵��� ������,Enemy�� ���̾�� 12��
        {
            //�ش� ��ü�� ��ũ��Ʈ�� �����Ͽ� ������(ü�°���)
            col.GetComponent<Character_Controller>().character.data.hp -= data.damage;
        }
    }
}
