using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    //매니저 클래스들의 관리를 위한 함수들
    public virtual void Manager_Start() { }
    public virtual void Manager_Update() { }
}
