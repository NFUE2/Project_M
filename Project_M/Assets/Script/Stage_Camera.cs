using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Camera : MonoBehaviour
{
    GameObject player; //플레이어 게임오브젝트를 담을 변수
    Vector3 origin; //초기의 카메라 위치
    Vector3 bossroom_Pos; //보스룸 위치

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); //플레이어 오브젝트 찾아서 입력
        origin = transform.position; //초기의 위치 입력
        bossroom_Pos = origin + new Vector3(180, 0, 0); //보스룸에서의 카메라 위치
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.P_boss || GameManager.instance.P_stage_clear) //보스전 이거나 스테이지를 클리어했을때 작동
        {
            if (Vector3.Distance(bossroom_Pos,transform.position ) > 0.5f) //만약 보스룸 카메라 위치와 지금의 위치가 멀 경우 작동
                transform.position = Vector3.Lerp(transform.position,bossroom_Pos,Time.deltaTime); //lerp를 이용하여 자연스럽게 카메라 위치이동
        }
        else
            transform.position = player.transform.position + origin; //일반 상태의 경우 카메라가 플레이어를 중심에 두고 이동
    }
}
