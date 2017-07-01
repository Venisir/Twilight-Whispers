using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            _animator.Play("Walk");
        if (Input.GetKeyDown(KeyCode.Keypad2))
            _animator.Play("Idle");
        if (Input.GetKeyDown(KeyCode.Keypad3))
            _animator.Play("Run");
        if (Input.GetKeyDown(KeyCode.Keypad4))
            _animator.Play("Dead");
        if (Input.GetKeyDown(KeyCode.Keypad5))
            _animator.Play("Jump");
        if (Input.GetKeyDown(KeyCode.Keypad6))
            _animator.Play("Attack(1)");
        if (Input.GetKeyDown(KeyCode.Keypad7))
            _animator.Play("Attack(2)");
        if (Input.GetKeyDown(KeyCode.Keypad8))
            _animator.Play("Get hit");

    }
}
