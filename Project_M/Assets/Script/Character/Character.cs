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
    public float charging; //플레이어의 차징 시간

    public float attack_distance; //몬스터 공격거리
    public bool player_search; //몬스터의 플레이어를 탐지유무

    public bool jumping; //점프중인지 아닌지 판별할 bool 변수
}

//전략패턴을 사용하기위한 부모클래스
public abstract class Character : MonoBehaviour
{
    float jumpPower = 13.0f; //점프력을 정해둔 변수
    public Data data; //캐릭터의 데이터를 정할 변수
    Rigidbody rigidbody; //캐릭터의 점프를 위한 클래스변수

    private void Start()
    {
        //Rigidbody컴포넌트가 존재할경우
        if (GetComponent<Rigidbody>() != null) 
            rigidbody = GetComponent<Rigidbody>();
    }

    //추상클래스를 생성하여 무조건 재정의 하도록 함,기본 정보를 설정하는 함수
    public abstract void initSetting();

    //몬스터가 사용할 가상함수, 플레이어 캐릭터는 사용하지 않음
    public virtual void monsterAction() { }

    //근접캐릭터들이 데미지를 주기 위한 함수
    public virtual void Damage(string layer)
    {
        foreach (Collider col in Physics.OverlapBox
            (transform.position + transform.right,
            new Vector3(0.5f, 0.5f, 0f),
            Quaternion.Euler(0, 0, 0),
            LayerMask.GetMask(layer))) //int로 사용하려햇으나 작동을 안했음,Enemy의 레이어는 12번
        {
            //해당 개체의 스크립트를 참조하여 데미지(체력감소)
            col.GetComponent<Character_Controller>().character.data.hp -= data.damage;
        }
    }

    public virtual void Charging_Attack(GameObject projectile, GameObject fire_pos) { }

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
        if(H != 0)
            transform.rotation = H > 0.0f ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 270, 0);

        //캐릭터가 바닥에 닿아있고 점프키가 눌려진다면 작동함
        if (!data.jumping && Input.GetAxis("Jump") == 1.0f)
            Jump();

        transform.position += (Vector3.right * H * data.speed) * Time.deltaTime;
    }

    //캐릭터가 공격할때 사용할 함수
    public virtual void Attack(GameObject projectile, GameObject fire_pos)
    {
        //data.animator.SetTrigger("Attack"); //공격 애니메이션 작동
        data.attack_timing = 0.0f; //일정 시간동안 공격기회를 잃음
    }

    private void OnCollisionEnter(Collision collision)
    {
        //땅과 접촉시 다시 점프를 할 수 있게 만듬
        if (data.jumping && collision.gameObject.layer == 10)
            data.jumping = false;
    }
}