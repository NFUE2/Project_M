using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�̱��� ���������� �ν��Ͻ�����
    public static GameManager instance;

    private void Awake()
    {
        instance = this; //�̱��� ����
        DontDestroyOnLoad(gameObject); //���� ���ӿ�����Ʈ�� ���� ����Ǿ �������� �ʰ���
    }
}
