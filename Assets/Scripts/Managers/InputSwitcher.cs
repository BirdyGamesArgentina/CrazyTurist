using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputSwitcher : MonoBehaviour
{
    public static InputSwitcher instance { get; private set; }

    public PlayerInput input = null;
    [SerializeField] string keyboardScheme = null;
    [SerializeField] string joystickScheme = null;

    public event Action OnChangeJoystick;
    public event Action OnChangeKeyboard;

    public bool isJoystick { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        input = FindObjectOfType<PlayerInput>();

        if (input.currentControlScheme == joystickScheme)
        {
            OnChangeJoystick?.Invoke();
            isJoystick = true;
        }
        else if (input.currentControlScheme == keyboardScheme)
        {
            OnChangeKeyboard?.Invoke();
            isJoystick = false;
        }
    }

    private void Start()
    {
        input = FindObjectOfType<PlayerInput>();
        if (input.currentControlScheme == joystickScheme)
        {
            OnChangeJoystick?.Invoke();
            isJoystick = true;
        }
        else if (input.currentControlScheme == keyboardScheme)
        {
            OnChangeKeyboard?.Invoke();
            isJoystick = false;
        }

        SceneLoader.Instance.OnEndLoadScene += FindNewInput;
    }

    private void Update()
    {
        if(input == null)
        {
            input = FindObjectOfType<PlayerInput>();
            return;
        }

        if (input.currentControlScheme == null) return;

        if (!isJoystick && input.currentControlScheme == joystickScheme)
        {
            OnChangeJoystick?.Invoke();
            isJoystick = true;
        }
        else if(isJoystick && input.currentControlScheme == keyboardScheme)
        {
            OnChangeKeyboard?.Invoke();
            isJoystick = false;
        }
    }

    public void FindNewInput()
    {
        input = FindObjectOfType<PlayerInput>();
    }
}
