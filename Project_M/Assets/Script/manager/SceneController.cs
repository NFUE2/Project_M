using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private bool changing = false; //씬이 변경중인지 아닌지 확인하는 변수

    private void Update()
    {
        //현재 씬의 넘버에 따라 작동하는 switch
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            //메인메뉴와 튜토리얼,캐릭터 선택창의 경우 엔터키를 누르면 씬이 넘어감
            case 0: case 1:
                if (Input.GetKeyDown(KeyCode.Return))
                    StartCoroutine(Scene_Change(0.0f));
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Return))
                    StartCoroutine(Scene_Change(3.0f));
                break;
            case 4: //랭킹씬에서의 작동
                if (GameManager.instance.P_game_end && !changing) //게임이 끝나고 씬이 변경중이 아니라면 작동
                    StartCoroutine(Scene_Change(5.0f));
                break;

            default: //스테이지 씬에서 작동
                if ((!GameManager.instance.P_player_survive || GameManager.instance.P_stage_clear)&& !changing) //플레이어가 사망하고 씬이 변경중이 아니라면 작동
                    StartCoroutine(Scene_Change(5.0f));
                break;
        }
    }

    //씬을 변경하기위한 함수,지연시간을 받고 그 뒤에 씬을 변경
    IEnumerator Scene_Change(float time)
    {
        changing = true; //씬이변경중입니다.
        yield return new WaitForSeconds(time);

        //내가 바꿀씬이 빌드씬세팅에서의 갯수보다 작다면 변경해줌
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //랭킹씬이 끝나면 게임을  초기화 해줍니다.
        else if (SceneManager.GetActiveScene().buildIndex == 4) 
        {
            GameManager.instance.P_player_survive = true;
            GameManager.instance.P_score = 0;
            GameManager.instance.P_stage_clear = false;
            GameManager.instance.P_game_end = false;
            SceneManager.LoadScene(0);
        }

        changing = false;
    }
}
