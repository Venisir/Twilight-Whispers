using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuController : MonoBehaviour
{
    [SerializeField]
    private Animation _animation;
    
    private void Start()
    {
        _animation.CrossFade("Staff Stance");
    }
}