using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�̱��� ���������� �ν��Ͻ�����
    public static GameManager instance;

    //ĳ���� ����â���� ������ ĳ���͸� ����
    GameObject player_character;

    private void Awake()
    {
        instance = this; //�̱��� ����
        DontDestroyOnLoad(gameObject); //���� ���ӿ�����Ʈ�� ���� ����Ǿ �������� �ʰ���
    }

    public GameObject player_obj { get { return player_character; } set { player_character = value; } }
}
