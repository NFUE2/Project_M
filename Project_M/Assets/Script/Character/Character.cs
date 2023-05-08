using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ĳ������ �⺻ �����͸� ���� ����ü
public struct Data
{
    public float hp;
    public float speed;
    public float damage;
    public bool jumping;
}

//���������� ����ϱ����� �θ�Ŭ����
public abstract class Character : MonoBehaviour
{
    //�������� ���ص� ����
    float jumpPower = 13.0f;
    public Data data;

    Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public abstract void initSetting();
    
    //������ ������ �Լ� Move�ȿ� �������� ���� ������ ���͵��� ���� ����ϱ�����
    private void Jump()
    {
        data.jumping = true;
        rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    //�̵� �������� ������ �����Լ�
    public virtual void Move()
    {
        //�¿� �Է����� ���� ������
        float H = Input.GetAxis("Horizontal");

        //ĳ���Ͱ� �ٴڿ� ����ְ� ����Ű�� �������ٸ� �۵���
        if (!data.jumping && Input.GetAxis("Jump") == 1.0f)
            Jump();

        transform.position += (Vector3.right * H * data.speed) * Time.deltaTime;
    }

    public virtual void Attack()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //���� ���˽� �ٽ� ������ �� �� �ְ� ����
        if (data.jumping && collision.gameObject.layer == 10)
            data.jumping = false;
    }
}
