using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum User
{
    Player,
    Com,
}

//ĳ������ �⺻ �����͸� ���� ����ü
public struct Data
{
    public User user;
    public Animator animator;
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

    public abstract void initSetting(Animator animator);

    //���Ͱ� ����� �����Լ�, �÷��̾� ĳ���ʹ� ������� ����
    public virtual void mosterAction() { }

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

        //Ű �Է¿� ���� ĳ������ �ü� ������ȯ
        if(H != 0)
            transform.rotation = H > 0.0f ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

        //ĳ���Ͱ� �ٴڿ� ����ְ� ����Ű�� �������ٸ� �۵���
        if (!data.jumping && Input.GetAxis("Jump") == 1.0f)
            Jump();

        transform.position += (Vector3.right * H * data.speed) * Time.deltaTime;
    }

    public virtual void Attack(GameObject projectile, GameObject fire_pos)
    {
        //data.animator.SetTrigger("Attack");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //���� ���˽� �ٽ� ������ �� �� �ְ� ����
        if (data.jumping && collision.gameObject.layer == 10)
            data.jumping = false;
    }
}