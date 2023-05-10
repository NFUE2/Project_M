using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Select_Character : MonoBehaviour
{
    #region Variable
    public Object[] character = new Object[3]; //선택할수 있는 캐릭터 종류
    public Image[] thumnail = new Image[3]; //플레이어에게 보여줄 이미지
    public GameObject choice; //플레이어가 선택하고 있는 캐릭터를 확인할 게임오브젝트

    int choice_num = 0; //현재 플레이어가 선택하고있는 캐릭터 넘버

    #endregion 

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            //유니티 프로젝트의 미리보기를 이미지로 사용하는방법
            Texture2D texture = AssetPreview.GetAssetPreview(character[i]);
            thumnail[i].sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        choice.GetComponent<RectTransform>().position += new Vector3(-500f, 0, 0); //캐릭터 선택 초기 위치
    }

    private void Update()
    {
        //엔터키를 누르면 게임매니저에 선택한 캐릭터를 저장
        if(Input.GetKeyDown(KeyCode.Return))
            GameManager.instance.player_obj = (GameObject)character[choice_num];

        //캐릭터를 정했으면 더 이상 작동못하게 함
        if (GameManager.instance.player_obj != null)
            return;

        //캐릭터를 정할수있게 방향키를 입력하여 선택하는 코드
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
