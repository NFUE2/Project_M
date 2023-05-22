using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Camera : MonoBehaviour
{
    GameObject player;
    Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player");
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position + origin;
    }
}
