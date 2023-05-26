using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tutorial_manager : Manager
{
    public Sprite[] up_button; //키를 떼었을때 이미지
    public Sprite[] down_button; //키를 눌렀을때 이미지
    public GameObject[] keyboard; //키를 보여주는 게임오브젝트

    // Start is called before the first frame update
    //키의 이미지를 설정하는 함수입니다.
    public override void Manager_Start()
    {
        for (int i = 0; i < 4; i++)
            keyboard[i].GetComponent<Image>().sprite = up_button[i];
    }

    // Update is called once per frame
    //튜토리얼에서 키의 입력을 보여주는 함수입니다.
    public override void Manager_Update()
    {
        keyboard[0].GetComponent<Image>().sprite = Input.GetAxis("Horizontal") < 0.0f ? down_button[0] : up_button[0];
        keyboard[1].GetComponent<Image>().sprite = Input.GetAxis("Horizontal") > 0.0f ? down_button[1] : up_button[1]; ;
        keyboard[3].GetComponent<Image>().sprite = Input.GetAxis("Attack") == 1.0f ? down_button[2] : up_button[2];
        keyboard[2].GetComponent<Image>().sprite = Input.GetAxis("Jump") == 1.0f ? down_button[3] : up_button[3];
    }
}
