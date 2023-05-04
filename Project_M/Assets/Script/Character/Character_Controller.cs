using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public Character character;
    public Rigidbody rigidbody;

    private void Start()
    {
        character.initSetting();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        character.Move(rigidbody, character.IsGrounded(transform.position));
    }
}
