using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private bool changing = false; //���� ���������� �ƴ��� Ȯ���ϴ� ����

    private void Update()
    {
        //���� ���� �ѹ��� ���� �۵��ϴ� switch
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            //���θ޴��� Ʃ�丮��,ĳ���� ����â�� ��� ����Ű�� ������ ���� �Ѿ
            case 0: case 1:
                if (Input.GetKeyDown(KeyCode.Return))
                    StartCoroutine(Scene_Change(0.0f));
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Return))
                    StartCoroutine(Scene_Change(3.0f));
                break;
            case 4: //��ŷ�������� �۵�
                if (GameManager.instance.P_game_end && !changing) //������ ������ ���� �������� �ƴ϶�� �۵�
                    StartCoroutine(Scene_Change(5.0f));
                break;

            default: //�������� ������ �۵�
                if ((!GameManager.instance.P_player_survive || GameManager.instance.P_stage_clear)&& !changing) //�÷��̾ ����ϰ� ���� �������� �ƴ϶�� �۵�
                    StartCoroutine(Scene_Change(5.0f));
                break;
        }
    }

    //���� �����ϱ����� �Լ�,�����ð��� �ް� �� �ڿ� ���� ����
    IEnumerator Scene_Change(float time)
    {
        changing = true; //���̺������Դϴ�.
        yield return new WaitForSeconds(time);

        //���� �ٲܾ��� ��������ÿ����� �������� �۴ٸ� ��������
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //��ŷ���� ������ ������  �ʱ�ȭ ���ݴϴ�.
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
