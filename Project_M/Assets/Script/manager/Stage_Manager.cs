using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Stage_Manager : Manager
{
    public GameObject[] create_pos;
    public GameObject[] create_unit;

    GameObject score;

    public GameObject Info_txt;

    [Range(0.0f, 20.0f)]
    public float[] distance;

    public override void Manager_Start()
    {
        Info_txt.GetComponent<Animator>().enabled = true;
        score = GameObject.Find("Score");
        GameObject player = Instantiate(GameManager.instance.P_player);
        player.transform.position = new Vector3(0, 2, 0);
        player.name = "Player";

        for (int i = 0; i < create_pos.Length; i++)
        {
            GameObject enemy = Instantiate(create_unit[i]);

            if (enemy.GetComponent<Enemy1>() != null || enemy.GetComponent<Enemy2>() != null)
                enemy.GetComponent<Character>().data.search_distance = distance[i];

            if (i == create_pos.Length - 1)
                enemy.gameObject.name = "Boss";
            
            enemy.transform.rotation = create_pos[i].transform.rotation;
            enemy.transform.localScale = new Vector3(2, 2, 2);
            enemy.transform.position = create_pos[i].transform.position;
        }
    }

    public override void Manager_Update()
    {
        score.GetComponent<TextMeshProUGUI>().text = GameManager.instance.P_score.ToString();

        if(GameObject.Find("Player").GetComponent<Character>().data.hp <= 0.0f || GameObject.Find("Boss").GetComponent<Character>().data.hp <= 0.0f)
        {
            if(GameObject.Find("Player").GetComponent<Character>().data.hp <= 0.0f)
                Info_txt.GetComponent<TextMeshProUGUI>().text = "GAME OVER";

            else if (GameObject.Find("Boss").GetComponent<Character>().data.hp <= 0.0f)
                Info_txt.GetComponent<TextMeshProUGUI>().text = "MISSON COMPLETE";

            Info_txt.GetComponent<Animator>().SetTrigger("Info");
        }
    }
}