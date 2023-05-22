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
    //public float charging; //�÷��̾��� ��¡ �ð�

    public Vector3 fire_pos; //���Ÿ� ���ݽ� �߻� ������
    public GameObject projectile; //���Ÿ� ���ݽ� ���ư� �߻�ü

    //������ƮǮ ��������� ����Ʈ 2��
    public List<GameObject> open_Obj; //����� ������ ���ӿ�����Ʈ
    public List<GameObject> close_Obj; //������� ���ӿ�����Ʈ

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
    public abstract void initSetting(Vector3 fire_pos, GameObject projectile);
    //���Ͱ� ����� �����Լ�, �÷��̾� ĳ���ʹ� ������� ����
    public virtual void Monster_Action() { }

    public void Attack()
    {
        Ray ray = new Ray(transform.position, transform.position + transform.right);
        bool check = Physics.Raycast(ray);
        data.animator.SetTrigger("Attack");

        string layer = gameObject.layer == 13 ? "Player" : "Enemy";

        if(check && data.attack_timing > data.attack_delay)
        {
            Close_Range_Attack(layer);
        }
        else if(!check && data.attack_timing > data.attack_delay)
        {
            try
            {
                Long_Range_Attack();
            }
            catch
            {
                Close_Range_Attack(layer);
            }
        }

        data.attack_timing = 0.0f;
    }

    //����ĳ���͵��� �������� �ֱ� ���� �Լ�
    private void Close_Range_Attack(string layer)
    {
        data.animator.SetTrigger("Close Attack");

        //������ ������ٵ� ��������ʾ� ��ĥ �� �ְ� �صξ �������� ���� �� ���ϼ��ְ� ��
        foreach (Collider col in Physics.OverlapBox
            (transform.position + transform.right,
            new Vector3(0.5f, 0.5f, 0f),
            Quaternion.Euler(0, 0, 0),
            LayerMask.GetMask(layer))) //int�� ����Ϸ������� �۵��� ������,Enemy�� ���̾�� 12��
        {
            //�ش� ��ü�� ��ũ��Ʈ�� �����Ͽ� ������(ü�°���)
            //col.GetComponent<Character_Controller>().character.data.hp -= data.damage;
        }
    }

    public virtual void Long_Range_Attack()
    {
        data.animator.SetTrigger("Long Range Attack");
    }

    //public virtual void Charging_Attack(GameObject projectile, GameObject fire_pos) { } //��¡����

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
        if (H != 0)
        {
            transform.rotation = H > 0.0f ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 270, 0);
            //if (!data.jumping) data.animator.SetBool("move", true);
        }
        //else
            //data.animator.SetBool("move", false);

        //ĳ���Ͱ� �ٴڿ� ����ְ� ����Ű�� �������ٸ� �۵���
        if (!data.jumping && Input.GetAxis("Jump") == 1.0f)
            Jump();

        transform.position += (Vector3.right * H * data.speed) * Time.deltaTime;
    }

    //ĳ���Ͱ� �����Ҷ� ����� �Լ�

    //������Ʈ Ǯ�� �̿��ϱ����� �Լ�
    protected List<GameObject> Create_projectile(GameObject projectile,Transform parent)
    {
        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(projectile);
            obj.SetActive(false);
            obj.transform.parent = parent;
            list.Add(obj);
        }

        return list;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //���� ���˽� �ٽ� ������ �� �� �ְ� ����
        if (data.jumping && collision.gameObject.layer == 10)
            data.jumping = false;
    }
}