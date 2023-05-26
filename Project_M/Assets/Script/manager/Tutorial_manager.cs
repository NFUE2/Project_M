using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tutorial_manager : Manager
{
    public Sprite[] up_button; //Ű�� �������� �̹���
    public Sprite[] down_button; //Ű�� �������� �̹���
    public GameObject[] keyboard; //Ű�� �����ִ� ���ӿ�����Ʈ

    // Start is called before the first frame update
    //Ű�� �̹����� �����ϴ� �Լ��Դϴ�.
    public override void Manager_Start()
    {
        for (int i = 0; i < 4; i++)
            keyboard[i].GetComponent<Image>().sprite = up_button[i];
    }

    // Update is called once per frame
    //Ʃ�丮�󿡼� Ű�� �Է��� �����ִ� �Լ��Դϴ�.
    public override void Manager_Update()
    {
        keyboard[0].GetComponent<Image>().sprite = Input.GetAxis("Horizontal") < 0.0f ? down_button[0] : up_button[0];
        keyboard[1].GetComponent<Image>().sprite = Input.GetAxis("Horizontal") > 0.0f ? down_button[1] : up_button[1]; ;
        keyboard[3].GetComponent<Image>().sprite = Input.GetAxis("Attack") == 1.0f ? down_button[2] : up_button[2];
        keyboard[2].GetComponent<Image>().sprite = Input.GetAxis("Jump") == 1.0f ? down_button[3] : up_button[3];
    }
}
