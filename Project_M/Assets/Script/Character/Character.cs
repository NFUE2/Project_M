using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//캐릭터를 움직일 사용자가 플레이어인지 컴퓨터인지 정함
public enum User
{
    Player,
    Com,
}

//캐릭터의 기본 데이터를 가질 구조체
public struct Data
{
    public User user; //사용자

    public GameObject Player; //몬스터가 가질 플레이어의 게임오브젝트 정보

    public Animator animator; //캐릭터의 애니메이션

    public float hp; //캐릭터의 체력
    public float speed; //캐릭터의 이동 스피드
    public float damage; //캐릭터 공격의 데미지

    public float attack_delay; //캐릭터의 공격간의 최소시간
    public float attack_timing; //캐릭터의 다음 공격까지의 시간
    //public float charging; //플레이어의 차징 시간

    public Vector3 fire_pos; //원거리 공격시 발사 포지션
    public GameObject projectile; //원거리 공격시 날아갈 발사체

    //오브젝트풀 사용을위한 리스트 2개
    public List<GameObject> open_Obj; //사용이 가능한 게임오브젝트
    public List<GameObject> close_Obj; //사용중인 게임오브젝트

    public float attack_distance; //몬스터 공격거리
    public bool player_search; //몬스터의 플레이어를 탐지유무

    public bool jumping; //점프중인지 아닌지 판별할 bool 변수
}

//전략패턴을 사용하기위한 부모클래스
public abstract class Character : MonoBehaviour
{
    float jumpPower = 15.0f; //점프력을 정해둔 변수
    public Data data; //캐릭터의 데이터를 정할 변수
    Rigidbody rigidbody; //캐릭터의 점프를 위한 클래스변수

    private void Start()
    {
        //Rigidbody컴포넌트가 존재할경우
        if (GetComponent<Rigidbody>() != null) 
            rigidbody = GetComponent<Rigidbody>();
    }

    //추상클래스를 생성하여 무조건 재정의 하도록 함,기본 정보를 설정하는 함수
    public abstract void initSetting(Vector3 fire_pos, GameObject projectile);
    //몬스터가 사용할 가상함수, 플레이어 캐릭터는 사용하지 않음
    public virtual void Monster_Action() { }

    //캐릭터가 공격할때 사용할 함수
    public void Attack()
    {
        Ray ray = new Ray(transform.position, transform.position + transform.right);
        bool check = Physics.Raycast(ray);

        data.attack_timing = 0.0f;

        if (check)
            data.animator.SetTrigger("Close Attack");

        else if(!check)
        {
            try
            {
                data.animator.SetTrigger("Long Range Attack");
            }
            catch
            {
                data.animator.SetTrigger("Close Attack");
            }
        }
    }

    //근접캐릭터들이 데미지를 주기 위한 함수
    public void Close_Range_Attack()
    {
        //레이어에 따른 공격 타겟
        string layer = gameObject.layer == 12 ? "Player" : "Enemy";

        //적들은 리지드바디를 사용하지않아 겹칠 수 있게 해두어서 범위내의 적은 다 죽일수있게 함
        foreach (Collider col in Physics.OverlapBox
            (transform.position + transform.right,
            new Vector3(1.5f, 0.5f, 0f),
            Quaternion.Euler(0, 0, 0),
            LayerMask.GetMask(layer))) //int로 사용하려햇으나 작동을 안했음,Enemy의 레이어는 12번
        {
            //해당 개체의 스크립트를 참조하여 데미지(체력감소)
            col.GetComponent<Character_Controller>().character.data.hp -= data.damage;
        }
    }

    public virtual void Long_Range_Attack()
    {

    }

    //public virtual void Charging_Attack(GameObject projectile, GameObject fire_pos) { } //차징공격

    //점프를 구현한 함수 Move안에 구현하지 않은 이유는 몬스터들이 따로 사용하기위함
    private void Jump()
    {
        data.jumping = true;
        rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    //이동 움직임을 구현한 가상함수,플레이어가 사용할 함수는 기본적으로 정의함
    public virtual void Move()
    {   
        //좌우 입력으로 인한 움직임
        float H = Input.GetAxis("Horizontal");

        //키 입력에 따른 캐릭터의 시선 방향전환
        if (H != 0)
        {
            transform.rotation = H > 0.0f ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 270, 0);
            if (!data.jumping) data.animator.SetBool("Move", true);
        }
        else
            data.animator.SetBool("Move", false);

        //캐릭터가 바닥에 닿아있고 점프키가 눌려진다면 작동함
        if (!data.jumping && Input.GetAxis("Jump") == 1.0f)
        {
            data.animator.SetTrigger("Jump");
            data.animator.SetBool("IsGround",false);
            Jump();
        }

        transform.position += (Vector3.right * H * data.speed) * Time.deltaTime;
    }


    //오브젝트 풀을 이용하기위한 함수
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
        //땅과 접촉시 다시 점프를 할 수 있게 만듬
        if (data.jumping && collision.gameObject.layer == 10)
        {
            data.jumping = false;
            data.animator.SetBool("IsGround", true);
        }
    }
}