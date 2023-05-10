using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public Character character; //� ĳ���͸� �޾ƿð������� ���� Ŭ��������
    public GameObject fire_Pos; //���Ÿ� ĳ������ ��� ����ü�� �߻�� ��ġ
    public GameObject projectile; //���Ÿ� ĳ������ ����ü ����

    Animator animator; //�ִϸ��̼�

    private void Start()
    {
        animator = GetComponent<Animator>(); //�� ĳ������ �ִϸ��̼�
        character.initSetting(); //�� ĳ������ �ʱ⺯�� ����
    }

    private void Update()
    {
        //�ð��� ������ ���ݱ�ȸ�� ����
        character.data.attack_timing += Time.deltaTime;

        //ü���� 0���Ϸ� �������� ����ϰ���
        if (character.data.hp < 0.0f)
        {
            //animator.SetTrigger("Dead"); //�ִϸ��̼ǿ��� ���ó��

            //��ũ��Ʈ�� ���̻� �۵����� �ʵ��� ó��
            gameObject.GetComponent<Character_Controller>().enabled = false;
            return;
        }
    }

    private void FixedUpdate()
    {
        //�ش� ��ũ��Ʈ�� �޾ƿ� character ������ �÷��̾����� ��ǻ������ ����
        if (character.data.user == User.Player) //�÷��̾��� ��� �۵�
        {
            //�ش�Ŭ�������� ������ �̵��Լ� �۵�
            character.Move();

            //AŰ�� ������ ��������
            if (Input.GetButtonDown("Attack"))
                character.Attack(projectile, fire_Pos);
            else if (Input.GetButton("Attack"))
                character.data.charging += Time.deltaTime;

            else if(Input.GetButtonUp("Attack"))
            {
                character.Charging_Attack(projectile, fire_Pos);
                character.data.charging = 0.0f;
            }
        }
        else //������ ��� �۵�
            character.monsterAction();
    }
}
