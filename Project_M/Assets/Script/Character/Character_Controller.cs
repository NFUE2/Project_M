using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public Character character;
    bool isgrounded = false;
    public Rigidbody rigidbody;

    private void Start()
    {
        character.initSetting();
    }

    private void Update()
    {
        isgrounded = character.IsGrounded(transform.position);
    }

    private void FixedUpdate()
    {
        character.Move(isgrounded,rigidbody);
    }
}
