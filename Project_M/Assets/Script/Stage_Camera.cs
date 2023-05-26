using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Camera : MonoBehaviour
{
    GameObject player; //�÷��̾� ���ӿ�����Ʈ�� ���� ����
    Vector3 origin; //�ʱ��� ī�޶� ��ġ
    Vector3 bossroom_Pos; //������ ��ġ

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); //�÷��̾� ������Ʈ ã�Ƽ� �Է�
        origin = transform.position; //�ʱ��� ��ġ �Է�
        bossroom_Pos = origin + new Vector3(180, 0, 0); //�����뿡���� ī�޶� ��ġ
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.P_boss || GameManager.instance.P_stage_clear) //������ �̰ų� ���������� Ŭ���������� �۵�
        {
            if (Vector3.Distance(bossroom_Pos,transform.position ) > 0.5f) //���� ������ ī�޶� ��ġ�� ������ ��ġ�� �� ��� �۵�
                transform.position = Vector3.Lerp(transform.position,bossroom_Pos,Time.deltaTime); //lerp�� �̿��Ͽ� �ڿ������� ī�޶� ��ġ�̵�
        }
        else
            transform.position = player.transform.position + origin; //�Ϲ� ������ ��� ī�޶� �÷��̾ �߽ɿ� �ΰ� �̵�
    }
}
