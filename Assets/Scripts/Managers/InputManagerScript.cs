using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerScript : MonoBehaviour
{
    private static InputManagerScript _inputManagerInstance = null;

    private InputMap _inputMap;

    [SerializeField] Vector2 _dPadInput;
    [SerializeField] bool _buttonSouthInput;
    [SerializeField] bool _buttonWestInput;
    [SerializeField] bool _buttonNorthInput;
    [SerializeField] bool _buttonEastInput;
    [SerializeField] bool _shoulderRInput;
    [SerializeField] bool _shoulderLInput;
    [SerializeField] bool _startInput;
    
    private void Awake()
    {
        InputManagerSingleton();
        SetUpInputs();
    }

    // Getters & Setters

    public Vector2 DPadInput { get { return _dPadInput; } set { _dPadInput = value; } }
    public bool ButtonSouthInput { get { return _buttonSouthInput; } set { _buttonSouthInput = value; } }
    public bool ButtonWestInput { get { return _buttonWestInput; } set { _buttonWestInput = value; } }
    public bool ButtonNorthInput { get { return _buttonNorthInput; } set { _buttonNorthInput = value; } }
    public bool ButtonEastInput { get { return _buttonEastInput; } set { _buttonEastInput = value; } }
    public bool ShoulderRInput { get { return _shoulderRInput; } set { _shoulderRInput = value; } }
    public bool ShoulderLInput { get { return _shoulderLInput; } set { _shoulderLInput = value; } }
    public bool StartInput { get { return _startInput; } set { _startInput = value; } }

    // Methods
    private void SetUpInputs()
    {
        _inputMap = new InputMap();
        _inputMap.Player.Enable();

        _inputMap.Player.DPad.performed += OnDpad;
        _inputMap.Player.ButtonSouth.performed += OnButtonSouth;
        _inputMap.Player.ButtonWest.performed += OnButtonWest;
        _inputMap.Player.ButtonNorth.performed += OnButtonNorth;
        _inputMap.Player.ButtonEast.performed += OnButtonEast;
        _inputMap.Player.ShoulderR.performed += OnShoulderR;
        _inputMap.Player.ShoulderL.performed += OnShoulderL;
        _inputMap.Player.StartButton.performed += OnStart;

        _inputMap.Player.DPad.canceled += OnDpad;
        _inputMap.Player.ButtonSouth.canceled += OnButtonSouth;
        _inputMap.Player.ButtonWest.canceled += OnButtonWest;
        _inputMap.Player.ButtonNorth.canceled += OnButtonNorth;
        _inputMap.Player.ButtonEast.canceled += OnButtonEast;
        _inputMap.Player.ShoulderR.canceled += OnShoulderR;
        _inputMap.Player.ShoulderL.canceled += OnShoulderL;
        _inputMap.Player.StartButton.canceled += OnStart;
    }
    private void UnsubscribeInputs()
    {
        _inputMap.Player.DPad.performed -= OnDpad;
        _inputMap.Player.ButtonSouth.performed -= OnButtonSouth;
        _inputMap.Player.ButtonWest.performed -= OnButtonWest;
        _inputMap.Player.ButtonNorth.performed -= OnButtonNorth;
        _inputMap.Player.ButtonEast.performed -= OnButtonEast;
        _inputMap.Player.ShoulderR.performed -= OnShoulderR;
        _inputMap.Player.ShoulderL.performed -= OnShoulderL;
        _inputMap.Player.StartButton.performed -= OnStart;
    }

    private void OnDpad(InputAction.CallbackContext context) 
    {
        DPadInput = context.ReadValue<Vector2>();
    }
    private void OnButtonSouth(InputAction.CallbackContext context) 
    {
        ButtonSouthInput = context.ReadValueAsButton();
    }
    private void OnButtonWest(InputAction.CallbackContext context) 
    {
        ButtonWestInput = context.ReadValueAsButton();
    }
    private void OnButtonNorth(InputAction.CallbackContext context) 
    {
        ButtonNorthInput = context.ReadValueAsButton();
    }
    private void OnButtonEast(InputAction.CallbackContext context) 
    {
        ButtonEastInput = context.ReadValueAsButton();
    }
    private void OnShoulderR(InputAction.CallbackContext context) 
    {
        ShoulderRInput = context.ReadValueAsButton();
    }
    private void OnShoulderL(InputAction.CallbackContext context) 
    {
        ShoulderLInput = context.ReadValueAsButton();
    }
    private void OnStart(InputAction.CallbackContext context) 
    {
        StartInput = context.ReadValueAsButton();
    }

    private void InputManagerSingleton()
    {
        if (_inputManagerInstance == null)
        {
            _inputManagerInstance = this;
        }
        else if (_inputManagerInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
