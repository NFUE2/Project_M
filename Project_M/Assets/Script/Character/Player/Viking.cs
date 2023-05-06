using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : Character
{
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void initSetting()
    {
        data.hp = 1.0f;
        data.speed = 10.0f;
        data.damage = 1.0f;
    }

    public override void Move(Rigidbody rigidbody,bool isgrounded)
    {
        base.Move(rigidbody, isgrounded);
    }
}
