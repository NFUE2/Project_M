using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public Character character;

    private void Start()
    {
        character.initSetting();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        character.Move();
    }
}
