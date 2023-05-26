using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Select_Character : MonoBehaviour
{
    #region Variable
    public Object[] character = new Object[3]; //�����Ҽ� �ִ� ĳ���� ����
    public Image[] thumnail = new Image[3]; //�÷��̾�� ������ �̹���
    Texture2D[] texture = new Texture2D[3]; //�÷��̾� �̸����� ����
    public GameObject choice; //�÷��̾ �����ϰ� �ִ� ĳ���͸� Ȯ���� ���ӿ�����Ʈ
    int choice_num = 0; //���� �÷��̾ �����ϰ��ִ� ĳ���� �ѹ�

    #endregion 

    private void Start()
    {
        //Resources������ ĳ���͵�� �̹����� ������
        character = Resources.LoadAll<Object>("Character/Player");
        texture = Resources.LoadAll<Texture2D>("Character/Player/Thumnail");

        for (int i = 0; i < 3; i++)
        {
            //������ ĳ���͵��� �����ؼ� ���� ����,�����ϴ� ������ ������� ���������
            GameObject player_obj = Instantiate((GameObject)character[i]);

            //����Ƽ ������Ʈ�� �̸����⸦ �̹����� ����ϴ¹��
            //Texture2D texture = AssetPreview.GetAssetPreview(player_obj);
            thumnail[i].sprite = Sprite.Create(texture[i], new Rect(0, 0, texture[i] .width, texture[i].height), new Vector2(0.5f, 0.5f)); //�̸����� ���
        }
        choice.GetComponent<RectTransform>().position += new Vector3(-500f, 0, 0); //ĳ���� ���� �ʱ� ��ġ
    }

    private void Update()
    {
        //����Ű�� ������ ������ ĳ���͸� �����ϰ� ���������ʰ���
        if (Input.GetKeyDown(KeyCode.Return))
            GameManager.instance.P_player = (GameObject)character[choice_num];

        //ĳ���͸� �������� �� �̻� �۵����ϰ� ��
        if (GameManager.instance.P_player != null)
            return;

        //ĳ���͸� ���Ҽ��ְ� ����Ű�� �Է��Ͽ� �����ϴ� �ڵ�
        if (Input.GetKeyDown(KeyCode.LeftArrow) && choice_num > 0)
        {
            choice.GetComponent<RectTransform>().position += new Vector3(-500f, 0, 0);
            choice_num--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && choice_num < 2)
        {
            choice.GetComponent<RectTransform>().position += new Vector3(500f, 0, 0);
            choice_num++;
        }
    }
}