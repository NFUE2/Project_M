using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Update()
    {
        //현재 씬의 넘버에 따라 작동하는 switch
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            //메인메뉴와 튜토리얼의 경우 엔터키를 누르면 씬이 넘어감
            case 0 : case 1 :
                if (Input.GetKeyDown(KeyCode.Return))
                    StartCoroutine(Scene_Change(0.0f));
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Return))
                    StartCoroutine(Scene_Change(3.0f));
                break;
            case 3:
                if (!GameManager.instance.P_player_survive)
                    StartCoroutine(Scene_Change(5.0f));
                break;
            //case 4:


        }
    }

    //씬을 변경하기위한 함수,지연시간을 받고 그 뒤에 씬을 변경
    IEnumerator Scene_Change(float time)
    {
        yield return new WaitForSeconds(time);

        //내가 바꿀씬이 빌드씬세팅에서의 갯수보다 작다면 변경해줌
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else if (SceneManager.GetActiveScene().buildIndex == 4)
            SceneManager.LoadScene(0);
    }
}
