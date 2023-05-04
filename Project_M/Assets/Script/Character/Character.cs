using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Data
{
    public float hp;
    public float speed;
    public float damage;
}

public abstract class Character : MonoBehaviour
{
    public Data data;
    public abstract void initSetting();
    

    public virtual void Move(Rigidbody rigidbody,bool isgrounded)
    {
        float H = Input.GetAxis("Horizontal");
        transform.position += (Vector3.right * H * data.speed) * Time.deltaTime;

        if (isgrounded && Input.GetAxis("Jump") == 1.0f)
        {
            rigidbody.AddForce(Vector3.up * 10.0f,ForceMode.Impulse);
        }

        if (transform.position.y > 4f)
            rigidbody.useGravity = true;
        else if(transform.position.y <= 1f)
            rigidbody.useGravity = false;

    }
    public virtual void Attack()
    {

    }

    public virtual  bool IsGrounded(Vector3 Pos)
    {
        Ray ray = new Ray(Pos + (Vector3.up * 0.2f),Vector3.down);
        if (Physics.Raycast(ray, 0.5f, LayerMask.GetMask("Ground")))
            return true;
        else
            return false;
    }
}
