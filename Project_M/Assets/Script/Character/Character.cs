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
    float gravity = -9.0f;

    public abstract void initSetting();
    public abstract void Attack();

    public virtual void Move(bool isgrounded,Rigidbody rigidbody)
    {
        float H = Input.GetAxis("Horizontal");
        transform.position += (Vector3.right * H * 5.0f) * Time.deltaTime;

        //if(isgrounded && Input.GetKeyDown(KeyCode.A))
            //rigidbody.AddForce();
    }

    public bool IsGrounded(Vector3 Pos)
    {
        Ray ray = new Ray(Pos + (Vector3.up * 0.2f),Vector3.down);

        if (Physics.Raycast(ray, 0.5f, LayerMask.GetMask("Ground")))
            return true;
        else
            return false;
    }

}
