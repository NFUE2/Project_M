using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : Character
{
    public override void Attack(GameObject projectile, GameObject fire_pos)
    {
        //Debug.Log("����");
        //base.Attack(projectile, fire_pos);
    }

    //ĳ������ �⺻ ������ ����
    public override void initSetting()
    {
        data.user = User.Player;

        data.animator = gameObject.GetComponent<Animator>();

        data.hp = 1.0f;
        data.speed = 10.0f;
        data.damage = 1.0f;

        data.charging = 0.0f;

        data.jumping = false;
    }

    public override void Move()
    {
        //�θ��� Move�Լ��� �۵�
        base.Move();
    }

    //�ִϸ��̼� �̺�Ʈ�� �̿��Ͽ� ������ �������� ��
    public override void Damage(string layer)
    {
        base.Damage("Enemy");
    }
}
