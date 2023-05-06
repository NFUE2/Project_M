using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    GameObject player_character;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameObject player_obj { get { return player_character; } set { player_character = value; } }
}
