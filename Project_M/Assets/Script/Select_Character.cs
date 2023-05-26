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
    Texture2D[] texture = new Texture2D[3]; //플레이어 미리보기 변수
    public GameObject choice; //플레이어가 선택하고 있는 캐릭터를 확인할 게임오브젝트
    int choice_num = 0; //현재 플레이어가 선택하고있는 캐릭터 넘버

    #endregion 

    private void Start()
    {
        //Resources폴더의 캐릭터들과 이미지를 가져옴
        character = Resources.LoadAll<Object>("Character/Player");
        texture = Resources.LoadAll<Texture2D>("Character/Player/Thumnail");

        for (int i = 0; i < 3; i++)
        {
            //가져온 캐릭터들을 복사해서 씬에 생성,생성하는 이유는 썸네일을 만들기위함
            GameObject player_obj = Instantiate((GameObject)character[i]);

            //유니티 프로젝트의 미리보기를 이미지로 사용하는방법
            //Texture2D texture = AssetPreview.GetAssetPreview(player_obj);
            thumnail[i].sprite = Sprite.Create(texture[i], new Rect(0, 0, texture[i] .width, texture[i].height), new Vector2(0.5f, 0.5f)); //미리보기 출력
        }
        choice.GetComponent<RectTransform>().position += new Vector3(-500f, 0, 0); //캐릭터 선택 초기 위치
    }

    private void Update()
    {
        //엔터키를 누르면 선택한 캐릭터를 복제하고 삭제되지않게함
        if (Input.GetKeyDown(KeyCode.Return))
            GameManager.instance.P_player = (GameObject)character[choice_num];

        //캐릭터를 정했으면 더 이상 작동못하게 함
        if (GameManager.instance.P_player != null)
            return;

        //캐릭터를 정할수있게 방향키를 입력하여 선택하는 코드
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