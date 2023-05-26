using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Controller : MonoBehaviour
{
    //매니저 클래스들의 작동을 위한 함수들
    public Manager manager;

    void Start()
    {
        manager.Manager_Start();
    }

    void Update()
    {
        manager.Manager_Update();
    }
}
