using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Data
{
    public float hp;
    public float speed;
    public float damage;
    public float attack_speed;
}

public abstract class Character : MonoBehaviour
{
    public Data data;

    public abstract void initSetting();
    public abstract void Attack();

    public virtual void Move()
    {
        Debug.Log("¿€µø¡ﬂ");
        float H = Input.GetAxis("Horizontal");
        Debug.Log(H);
        transform.position += (Vector3.right * H * 5.0f) * Time.deltaTime;
    }
}
