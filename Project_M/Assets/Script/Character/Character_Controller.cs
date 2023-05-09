using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public Character character;
    public GameObject fire_Pos;
    public GameObject projectile;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>(); //�� ĳ������ �ִϸ��̼�
        character.initSetting(animator); //�� ĳ������ �ʱ⺯�� ����
    }

    private void Update()
    {
        //ü���� 0���Ϸ� �������� ����ϰ���
        if (character.data.hp < 0.0f)
        {
            //animator.SetTrigger("Dead");
            gameObject.GetComponent<Character_Controller>().enabled = false;
            return;
        }
    }

    private void FixedUpdate()
    {
        //�ش� ��ũ��Ʈ�� �޾ƿ� character ������ �÷��̾����� ��ǻ������ ����
        if (character.data.user == User.Player)
        {
            //�ش�Ŭ�������� ������ �̵��Լ� �۵�
            character.Move();

            //AŰ�� ������ ��������
             if (Input.GetAxis("Attack") == 1.0f)
                character.Attack(projectile, fire_Pos);
        }
        else
            character.mosterAction();

    }
}
