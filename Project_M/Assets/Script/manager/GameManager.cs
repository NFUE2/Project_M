using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�̱��� ���������� �ν��Ͻ�����
    public static GameManager instance;
    private int score;

    private void Awake()
    {
        instance = this; //�̱��� ����
        DontDestroyOnLoad(gameObject); //���� ���ӿ�����Ʈ�� ���� ����Ǿ �������� �ʰ���
    }

    public int P_score { get { return score; } set { score += value; } }
}
