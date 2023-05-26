using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������������ ĳ���͸� ����ϱ����� Ŭ����
public class Character_Controller : MonoBehaviour
{
    public Character character; //� ĳ���͸� �޾ƿð������� ���� Ŭ��������

    public GameObject fire_Pos; //���Ÿ� ĳ������ ��� ����ü�� �߻�� ��ġ
    public GameObject projectile; //���Ÿ� ĳ������ ����ü ����

    public AudioClip dead_sound;

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
    }

    private void FixedUpdate()
    {
        //ü���� 0���Ϸ� �������� ����ϰ���
        if (character.data.hp <= 0.0f)
        {
            GetComponent<AudioSource>().clip = dead_sound;
            GetComponent<AudioSource>().Play();

            character.data.animator.SetTrigger("Dead"); //�ִϸ��̼ǿ��� ���ó��
            if (gameObject.layer == 13) //�÷��̾ ����Ұ��
            {
                GameManager.instance.P_player_survive = false;
                if (GameManager.instance.P_boss) GameManager.instance.P_boss = false;
            }

            else if (gameObject.layer == 12) //���� ����Ұ��
            {
                GameManager.instance.P_score = character.data.score;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;

                if (gameObject.name == "Boss")
                {
                    GameManager.instance.P_stage_clear = true;
                    GameManager.instance.P_boss = false;
                }
            }
            gameObject.GetComponent<Character>().enabled = false;
            gameObject.GetComponent<Character_Controller>().enabled = false;

            //��ũ��Ʈ�� ���̻� �۵����� �ʵ��� ó��
            return;
        }

        //�ش� ��ũ��Ʈ�� �޾ƿ� character ������ �÷��̾����� ��ǻ������ ����
        if (character.data.user == User.Player) //�÷��̾��� ��� �۵�
        {
            //�ش�Ŭ�������� ������ �̵��Լ� �۵�
            character.Move();

            //AŰ�� ������ ��������
            if (Input.GetAxis("Attack") == 1.0f && character.data.attack_delay < character.data.attack_timing)
                character.Attack();
        }
        else //������ ��� �۵�
            character.Monster_Action();
    }
}
