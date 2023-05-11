using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ĳ���͸� ������ ����ڰ� �÷��̾����� ��ǻ������ ����
public enum User
{
    Player,
    Com,
}

//ĳ������ �⺻ �����͸� ���� ����ü
public struct Data
{
    public User user; //�����

    public GameObject Player; //���Ͱ� ���� �÷��̾��� ���ӿ�����Ʈ ����

    public Animator animator; //ĳ������ �ִϸ��̼�

    public float hp; //ĳ������ ü��
    public float speed; //ĳ������ �̵� ���ǵ�
    public float damage; //ĳ���� ������ ������

    public float attack_delay; //ĳ������ ���ݰ��� �ּҽð�
    public float attack_timing; //ĳ������ ���� ���ݱ����� �ð�
    public float charging; //�÷��̾��� ��¡ �ð�

    public float attack_distance; //���� ���ݰŸ�
    public bool player_search; //������ �÷��̾ Ž������

    public bool jumping; //���������� �ƴ��� �Ǻ��� bool ����
}

//���������� ����ϱ����� �θ�Ŭ����
public abstract class Character : MonoBehaviour
{
    float jumpPower = 13.0f; //�������� ���ص� ����
    public Data data; //ĳ������ �����͸� ���� ����
    Rigidbody rigidbody; //ĳ������ ������ ���� Ŭ��������

    private void Start()
    {
        //Rigidbody������Ʈ�� �����Ұ��
        if (GetComponent<Rigidbody>() != null) 
            rigidbody = GetComponent<Rigidbody>();
    }

    //�߻�Ŭ������ �����Ͽ� ������ ������ �ϵ��� ��,�⺻ ������ �����ϴ� �Լ�
    public abstract void initSetting();

    //���Ͱ� ����� �����Լ�, �÷��̾� ĳ���ʹ� ������� ����
    public virtual void monsterAction() { }

    //����ĳ���͵��� �������� �ֱ� ���� �Լ�
    public virtual void Damage(string layer)
    {
        foreach (Collider col in Physics.OverlapBox
            (transform.position + transform.right,
            new Vector3(0.5f, 0.5f, 0f),
            Quaternion.Euler(0, 0, 0),
            LayerMask.GetMask(layer))) //int�� ����Ϸ������� �۵��� ������,Enemy�� ���̾�� 12��
        {
            //�ش� ��ü�� ��ũ��Ʈ�� �����Ͽ� ������(ü�°���)
            col.GetComponent<Character_Controller>().character.data.hp -= data.damage;
        }
    }

    public virtual void Charging_Attack(GameObject projectile, GameObject fire_pos) { }

    //������ ������ �Լ� Move�ȿ� �������� ���� ������ ���͵��� ���� ����ϱ�����
    private void Jump()
    {
        data.jumping = true;
        rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    //�̵� �������� ������ �����Լ�,�÷��̾ ����� �Լ��� �⺻������ ������
    public virtual void Move()
    {   
        //�¿� �Է����� ���� ������
        float H = Input.GetAxis("Horizontal");

        //Ű �Է¿� ���� ĳ������ �ü� ������ȯ
        if(H != 0)
            transform.rotation = H > 0.0f ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 270, 0);

        //ĳ���Ͱ� �ٴڿ� ����ְ� ����Ű�� �������ٸ� �۵���
        if (!data.jumping && Input.GetAxis("Jump") == 1.0f)
            Jump();

        transform.position += (Vector3.right * H * data.speed) * Time.deltaTime;
    }

    //ĳ���Ͱ� �����Ҷ� ����� �Լ�
    public virtual void Attack(GameObject projectile, GameObject fire_pos)
    {
        //data.animator.SetTrigger("Attack"); //���� �ִϸ��̼� �۵�
        data.attack_timing = 0.0f; //���� �ð����� ���ݱ�ȸ�� ����
    }

    private void OnCollisionEnter(Collision collision)
    {
        //���� ���˽� �ٽ� ������ �� �� �ְ� ����
        if (data.jumping && collision.gameObject.layer == 10)
            data.jumping = false;
    }
}