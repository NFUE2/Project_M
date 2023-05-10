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
    public GameObject choice; //�÷��̾ �����ϰ� �ִ� ĳ���͸� Ȯ���� ���ӿ�����Ʈ

    int choice_num = 0; //���� �÷��̾ �����ϰ��ִ� ĳ���� �ѹ�

    #endregion 

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            //����Ƽ ������Ʈ�� �̸����⸦ �̹����� ����ϴ¹��
            Texture2D texture = AssetPreview.GetAssetPreview(character[i]);
            thumnail[i].sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        choice.GetComponent<RectTransform>().position += new Vector3(-500f, 0, 0); //ĳ���� ���� �ʱ� ��ġ
    }

    private void Update()
    {
        //����Ű�� ������ ���ӸŴ����� ������ ĳ���͸� ����
        if(Input.GetKeyDown(KeyCode.Return))
            GameManager.instance.player_obj = (GameObject)character[choice_num];

        //ĳ���͸� �������� �� �̻� �۵����ϰ� ��
        if (GameManager.instance.player_obj != null)
            return;

        //ĳ���͸� ���Ҽ��ְ� ����Ű�� �Է��Ͽ� �����ϴ� �ڵ�
        if(Input.GetKeyDown(KeyCode.LeftArrow) && choice_num > 0)
        {
            choice.GetComponent<RectTransform>().position += new Vector3(-500f, 0, 0);
            choice_num--;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && choice_num < 2)
        {
            choice.GetComponent<RectTransform>().position += new Vector3(500f, 0, 0);
            choice_num++;
        }
    }
}
