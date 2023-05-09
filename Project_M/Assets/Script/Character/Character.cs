using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum User
{
    Player,
    Com,
}

//캐릭터의 기본 데이터를 가질 구조체
public struct Data
{
    public User user;
    public Animator animator;
    public float hp;
    public float speed;
    public float damage;
    public bool jumping;
}

//전략패턴을 사용하기위한 부모클래스
public abstract class Character : MonoBehaviour
{
    //점프력을 정해둔 변수
    float jumpPower = 13.0f;
    public Data data;
    Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public abstract void initSetting(Animator animator);

    //몬스터가 사용할 가상함수, 플레이어 캐릭터는 사용하지 않음
    public virtual void mosterAction() { }

    //점프를 구현한 함수 Move안에 구현하지 않은 이유는 몬스터들이 따로 사용하기위함
    private void Jump()
    {
        data.jumping = true;
        rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    //이동 움직임을 구현한 가상함수
    public virtual void Move()
    {   
        //좌우 입력으로 인한 움직임
        float H = Input.GetAxis("Horizontal");

        //키 입력에 따른 캐릭터의 시선 방향전환
        if(H != 0)
            transform.rotation = H > 0.0f ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

        //캐릭터가 바닥에 닿아있고 점프키가 눌려진다면 작동함
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
        //땅과 접촉시 다시 점프를 할 수 있게 만듬
        if (data.jumping && collision.gameObject.layer == 10)
            data.jumping = false;
    }
}