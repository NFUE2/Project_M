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

        //Debug.Log(isgrounded);
        if (Input.GetAxis("Jump") == 1.0f)
        {
            Debug.Log("มกวม");
            rigidbody.AddForce(Vector3.up * 5.0f,ForceMode.Impulse);
        }
    }
    public virtual void Attack()
    {

    }

    public bool IsGrounded(Vector3 Pos)
    {
        Ray ray = new Ray(Pos + (Vector3.up * 0.2f),Pos + Vector3.down * 1.1f);

        if (Physics.Raycast(ray, 0.5f, LayerMask.GetMask("Ground")))
            return true;
        else
            return false;
    }
}
