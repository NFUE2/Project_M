using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Camera : MonoBehaviour
{
    GameObject player;
    Vector3 origin;
    Vector3 bossroom_Pos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        origin = transform.position;
        bossroom_Pos = origin + new Vector3(180, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.P_boss || GameManager.instance.P_stage_clear)
        {
            if (Vector3.Distance(bossroom_Pos,transform.position ) > 0.5f)
                transform.position = Vector3.Lerp(transform.position,bossroom_Pos,Time.deltaTime);
        }
        else
            transform.position = player.transform.position + origin;
    }
}
