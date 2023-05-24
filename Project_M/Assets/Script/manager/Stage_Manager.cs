using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Manager : Manager
{
    public GameObject[] create_pos;
    public GameObject[] create_unit;

    public override void Manager_Start()
    {
        GameObject player = Instantiate(GameManager.instance.P_player);
        player.transform.position = new Vector3(0, 2, 0);
        player.name = "Player";

        //for (int i = 0; i < create_pos.Length; i++)
        //{
        //    GameObject enemy = Instantiate(create_unit[i]);
        //    enemy.transform.position = create_pos[i].transform.position;
        //}
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < create_pos.Length; i++)
        {
            Gizmos.DrawSphere(create_pos[i].transform.position,1f);
        }
    }
}
