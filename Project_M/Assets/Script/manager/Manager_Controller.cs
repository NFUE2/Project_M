using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Controller : MonoBehaviour
{
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
