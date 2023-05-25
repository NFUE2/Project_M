using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private bool changing = false;

    private void Update()
    {
        //���� ���� �ѹ��� ���� �۵��ϴ� switch
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            //���θ޴��� Ʃ�丮���� ��� ����Ű�� ������ ���� �Ѿ
            case 0: case 1:
                GameManager.instance.P_player_survive = true;
                GameManager.instance.P_score = 0;
                GameManager.instance.P_stage_clear = false;
                GameManager.instance.P_game_end = false;

                if (Input.GetKeyDown(KeyCode.Return))
                    StartCoroutine(Scene_Change(0.0f));
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Return))
                    StartCoroutine(Scene_Change(3.0f));
                break;
            case 4:
                if (GameManager.instance.P_game_end && !changing)
                    StartCoroutine(Scene_Change(5.0f));
                break;

            default:
                if (!GameManager.instance.P_player_survive && !changing)
                    StartCoroutine(Scene_Change(5.0f));
                break;
        }
    }

    public bool Set_changing {set { changing = value; } }

    //���� �����ϱ����� �Լ�,�����ð��� �ް� �� �ڿ� ���� ����
    IEnumerator Scene_Change(float time)
    {
        changing = true;
        yield return new WaitForSeconds(time);

        //���� �ٲܾ��� ��������ÿ����� �������� �۴ٸ� ��������
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else if (SceneManager.GetActiveScene().buildIndex == 4)
            SceneManager.LoadScene(0);

        changing = false;
    }
}
