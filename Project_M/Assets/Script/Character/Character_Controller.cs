using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public Character character; //� ĳ���͸� �޾ƿð������� ���� Ŭ��������

    public GameObject fire_Pos; //���Ÿ� ĳ������ ��� ����ü�� �߻�� ��ġ
    public GameObject projectile; //���Ÿ� ĳ������ ����ü ����

    private void Start()
    {
        if (fire_Pos == null && projectile == null)
            character.initSetting(Vector3.zero, null);
        else
            character.initSetting(fire_Pos.transform.position, projectile); //�� ĳ������ �ʱ⺯�� ����
    }

    private void Update()
    {
        //�ð��� ������ ���ݱ�ȸ�� ����
        character.data.attack_timing += Time.deltaTime;

        //ü���� 0���Ϸ� �������� ����ϰ���
        if (character.data.hp <= 0.0f)
        {
            character.data.animator.SetTrigger("Dead"); //�ִϸ��̼ǿ��� ���ó��

            if (gameObject.layer == 13) //�÷��̾ ����Ұ��
                GameManager.instance.P_player_survive = false;

            else if (gameObject.layer == 12) //���� ����Ұ��
            {
                GameManager.instance.P_score = character.data.score;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
            gameObject.GetComponent<Character_Controller>().enabled = false;

            //��ũ��Ʈ�� ���̻� �۵����� �ʵ��� ó��
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
            if (Input.GetAxis("Attack") == 1.0f && character.data.attack_delay < character.data.attack_timing)
            {
                character.Attack();
            }

            ////AŰ�� ������ ������ ��¡������ �غ�
            //else if (Input.GetButton("Attack"))
            //    character.data.charging += Time.deltaTime;

            ////��¡�� ������� �ǰ� Ű�� ���� ��¡������ ����
            //else if(Input.GetButtonUp("Attack"))
            //{
            //    character.Charging_Attack(projectile, fire_Pos);
            //    character.data.charging = 0.0f;
            //}
        }
        else //������ ��� �۵�
            character.Monster_Action();
    }
}
