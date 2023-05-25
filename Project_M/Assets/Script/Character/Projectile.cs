using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Character
{
    public override void initSetting(Vector3 fire_pos, GameObject projectile)
    {
        data.speed = 10.0f;
    }
}
