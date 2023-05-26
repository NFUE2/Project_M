using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Stage_Manager : Manager
{
    //������ Ž�������� �����մϴ�.
    [Range(0.0f, 30.0f)]
    public float[] distance;

    public GameObject[] create_unit; //������ �� ���ӿ�����Ʈ�� �Է¹޽��ϴ�.
    public GameObject[] create_pos;  //������ �� ���ӿ�����Ʈ�� ��ġ�� �Է� �޽��ϴ�.

    public BoxCollider boxCollider; //�������� ����� ���� �Է¹޽��ϴ�.

    public AudioClip clear_voice; //���̽� ������ �Է¹޽��ϴ�.
    AudioSource audio; //������ ������ AudioSource�Դϴ�.
    bool voicechk = false; //������ �۵��ߴ��� ���ߴ��� Ȯ���ϴ� �����Դϴ�.

    GameObject score; //���ھ �����ִ� �����Դϴ�.
    public GameObject Info_txt; //�������� �ȳ������Դϴ�.


    public override void Manager_Start()
    {
        audio = GetComponent<AudioSource>(); //AudioSource�Ҵ�

        Info_txt.GetComponent<Animator>().enabled = true; //�ִϸ��̼� �۵�

        score = GameObject.Find("Score"); //���ӿ�����Ʈ �Ҵ�

        GameObject player = Instantiate(GameManager.instance.P_player); //�÷��̾� ĳ���� ����
        player.transform.rotation = Quaternion.Euler(0, 90, 0); //�÷��̾� ĳ���� ȸ���� �Է�
        player.transform.position = new Vector3(0, 2, 0); //�÷��̾� ũ�� ����
        player.name = "Player"; //�÷��̾� �̸� ����

        for (int i = 0; i < create_pos.Length; i++) //������ ���� ����ŭ �ݺ��մϴ�
        {
            GameObject enemy = Instantiate(create_unit[i]); //���� ����

            enemy.GetComponent<Character>().data.search_distance = distance[i]; //Ž�������� �Է�

            if (i == create_pos.Length - 1) //������ ���� ������ �����մϴ�.
                enemy.gameObject.name = "Boss";
            
            //������ ���� ȸ��,ũ��,��ġ�� �Է�
            enemy.transform.rotation = create_pos[i].transform.rotation;
            enemy.transform.localScale = new Vector3(2, 2, 2);
            enemy.transform.position = create_pos[i].transform.position;
        }
    }

    public override void Manager_Update()
    {
        score.GetComponent<TextMeshProUGUI>().text = GameManager.instance.P_score.ToString(); //���ھ �Է�

        if(GameObject.Find("Player").GetComponent<Character>().data.hp <= 0.0f || GameObject.Find("Boss").GetComponent<Character>().data.hp <= 0.0f) //���� �÷��̾ ����ϰų� ������ ����Ұ�� �۵�
        {
            if(GameObject.Find("Player").GetComponent<Character>().data.hp <= 0.0f) //�÷��̾ ����ϸ� ���ӿ��� �ؽ�Ʈ �Է�
                Info_txt.GetComponent<TextMeshProUGUI>().text = "GAME OVER";

            else if (GameObject.Find("Boss").GetComponent<Character>().data.hp <= 0.0f) //������ ����ϸ� �̼Ǽ��� �ؽ�Ʈ �� ���� ���
            {
                VoicePlay();
                Info_txt.GetComponent<TextMeshProUGUI>().text = "MISSON COMPLETE";
            }

            Info_txt.GetComponent<Animator>().SetTrigger("Info"); //�ȳ� �ؽ�Ʈ �ִϸ��̼� ����
        }

        if (Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Boss").transform.position) < 25.0f) //������ ���Խ� �۵�
        {
            boxCollider.enabled = true;  //�÷��̾ ��������ϰ� �� ����
            if(!GameManager.instance.P_stage_clear) //Ŭ�����������ߴٸ� ������ ����
                GameManager.instance.P_boss = true;
        }
    }

    void VoicePlay() //���� ����
    {
        if (voicechk) return;
        audio.clip = clear_voice;
        audio.Play();
        voicechk = true;
    }
}