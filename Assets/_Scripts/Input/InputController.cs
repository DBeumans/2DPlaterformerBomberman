using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Splitten.Extensions;

public class InputController : Singleton<InputController>
{
    private Vector2 input;
    public Vector2 GetInputDirection
    {
        get => this.input;
    }

    public float GetInputHorizontal
    {
        get => this.input.x;
    }

    public float GetInputVertical
    {
        get => this.input.y;
    }

    private VirtualJoystick virtualJoystick;

    private void Start()
    {
        if(VirtualJoystick.Instance == true)
        {
            this.virtualJoystick = VirtualJoystick.Instance;
        }
    }

    private void Update()
    {
        if(!Application.isEditor)
        {
            this.input = this.virtualJoystick.JoystickOutput;
            return;
        }

        this.input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
    }


}