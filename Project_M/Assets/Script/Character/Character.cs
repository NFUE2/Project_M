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

    public Vector3 fire_pos; //���Ÿ� ���ݽ� �߻� ������
    public GameObject projectile; //���Ÿ� ���ݽ� ���ư� �߻�ü

    //������ƮǮ ��������� ����Ʈ 2��
    public List<GameObject> open_Obj; //����� ������ ���ӿ�����Ʈ
    public List<GameObject> close_Obj; //������� ���ӿ�����Ʈ

    public float search_distance; //���� Ž���Ÿ�
    public int score; //���� ����� ��� ����
    public float attack_distance; //���� ���ݰŸ�
    public bool player_search; //������ �÷��̾ Ž������

    public bool jumping; //���������� �ƴ��� �Ǻ��� bool ����

    public AudioSource audio;
}

//���������� ����ϱ����� �θ�Ŭ����
public abstract class Character : MonoBehaviour
{
    public AudioClip close_attack_sound;
    public AudioClip long_range_sound;

    public GameObject close_attack_effect;
    public GameObject long_range_effect;

    float jumpPower = 15.0f; //�������� ���ص� ����
    public Data data; //ĳ������ �����͸� ���� ����
    Rigidbody rigidbody; //ĳ������ ������ ���� Ŭ���� ����

    private void Start()
    {
        //Rigidbody������Ʈ�� �����Ұ��
        if (GetComponent<Rigidbody>() != null) 
            rigidbody = GetComponent<Rigidbody>();

        data.audio = GetComponent<AudioSource>();
    }

    //�߻�Ŭ������ �����Ͽ� ������ ������ �ϵ��� ��,�⺻ ������ �����ϴ� �Լ�
    public abstract void initSetting(Vector3 fire_pos , GameObject projectile);
    //���Ͱ� ����� �����Լ�, �÷��̾� ĳ���ʹ� ������� ����
    public virtual void Monster_Action() { }

    //ĳ���Ͱ� �����Ҷ� ����� �Լ�
    public void Attack()
    {
        Ray ray = new Ray(transform.position, transform.position + transform.forward); 
        bool check = Physics.Raycast(ray); //���̸� �̿��Ͽ� �������� �ȿ� ���� �ִ��� ������ Ȯ���մϴ�.

        data.attack_timing = 0.0f; //������ �ߴٸ� ��Ÿ���� �����ϴ�.

        if (check) //���� �������
            data.animator.SetTrigger("Close Attack");

        else if(!check) //���� �ָ��������
        {
            try
            {
                data.animator.SetTrigger("Long Range Attack"); 
            }
            catch
            {
                data.animator.SetTrigger("Close Attack"); //����ĳ���ʹ� ���Ÿ� ������ �Ҽ� �����Ƿ� �۵�
            }
        }
    }

    //����ĳ���͵��� �������� �ֱ� ���� �Լ�
    public void Close_Range_Attack()
    {
        if (close_attack_effect != null)
            close_attack_effect.SetActive(true);

        //���̾ ���� ���� Ÿ��
        string layer = gameObject.layer == 12 ? "Player" : "Enemy";
        Vector3 defalutpos = transform.position + new Vector3(0, 1.5f, 0);

        //������ ������ٵ� ��������ʾ� ��ĥ �� �ְ� �صξ �������� ���� �� ���ϼ��ְ� ��
        foreach (Collider col in Physics.OverlapBox
            (defalutpos + transform.forward * 2,
            new Vector3(1.5f, 2f, 0f),
            Quaternion.Euler(0, 0, 0),
            LayerMask.GetMask(layer))) //int�� ����Ϸ������� �۵��� ������,Enemy�� ���̾�� 12��
        {
            if (close_attack_sound != null) //���������� Ÿ�� ���尡 �ִٸ� �۵�
            {
                //Ÿ���� �۵�
                data.audio.clip = close_attack_sound; 
                data.audio.Play();
            }

            //�ش� ��ü�� ��ũ��Ʈ�� �����Ͽ� ������(ü�°���)
            col.GetComponent<Character_Controller>().character.data.hp -= data.damage;
        }
    }

    public virtual void Long_Range_Attack()
    {

    }

    //������ ������ �Լ� Move�ȿ� �������� ���� ������ ���͵��� ���� ����ϱ�����
    IEnumerator Jump()
    {
        data.jumping = true;

        yield return new WaitForSeconds(0.1f);
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
            if (!data.jumping) data.animator.SetBool("Move", true);
        }
        else
            data.animator.SetBool("Move", false);

        //ĳ���Ͱ� �ٴڿ� ����ְ� ����Ű�� �������ٸ� �۵���
        if (!data.jumping && Input.GetAxis("Jump") == 1.0f)
        {
            data.animator.SetTrigger("Jump");
            data.animator.SetBool("IsGround",false);
            StartCoroutine(Jump());
        }

        transform.position += (Vector3.right * H * data.speed) * Time.deltaTime;
    }


    //������Ʈ Ǯ�� �̿��ϱ����� �Լ�
    protected List<GameObject> Set_projectile(GameObject projectile,Transform parent)
    {
        List<GameObject> list = new List<GameObject>();

        if(data.close_Obj.Count == 10)
        {
            for (int i = 0; i < 10; i++)
            {
                data.open_Obj.Add(data.close_Obj[i]);
                data.close_Obj.Remove(data.close_Obj[i]);
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(projectile);
                obj.SetActive(false);
                obj.transform.parent = parent;
                list.Add(obj);
            }
        }
        return list;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //���� ���˽� �ٽ� ������ �� �� �ְ� ����
        if (data.jumping && collision.gameObject.layer == 10)
        {
            data.jumping = false;
            data.animator.SetBool("IsGround", true);
        }
   }
}