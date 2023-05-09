using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Update()
    {
        //���� ���� �ѹ��� ���� �۵��ϴ� switch
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            //���θ޴��� Ʃ�丮���� ��� ����Ű�� ������ ���� �Ѿ
            case 0 : case 1 :
                if (Input.GetKeyDown(KeyCode.Return))
                    StartCoroutine(Scene_Change(0.0f));
                //Scene_Change();
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Return))
                    StartCoroutine(Scene_Change(3.0f));
                break;
        }
    }

    //���� �����ϱ����� �Լ�
    IEnumerator Scene_Change(float time)
    {
        yield return new WaitForSeconds(time);

        //���� �ٲܾ��� ��������ÿ����� �������� �۴ٸ� ��������
            if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
