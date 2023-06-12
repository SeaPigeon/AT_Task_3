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
        SetUpInputMap();
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
    private void SetUpInputMap()
    {
        _inputMap = new InputMap();
        EnableInputMap(ref _inputMap.UI);
        _inputMap.Enable();
    }

    private void EnableInputMap(ref InputMap map)
    {
        _inputMap.Disable();
        map.Enable();
    }

    private void SetUpInputs()
    {
        
        //_inputMap.UI.Enable();

        _inputMap.UI.DPad.performed += OnDpad;
        _inputMap.UI.ButtonSouth.performed += OnButtonSouth;
        _inputMap.UI.ButtonWest.performed += OnButtonWest;
        _inputMap.UI.ButtonNorth.performed += OnButtonNorth;
        _inputMap.UI.ButtonEast.performed += OnButtonEast;
        _inputMap.UI.ShoulderR.performed += OnShoulderR;
        _inputMap.UI.ShoulderL.performed += OnShoulderL;
        _inputMap.UI.StartButton.performed += OnStart;

        _inputMap.UI.DPad.canceled += OnDpad;
        _inputMap.UI.ButtonSouth.canceled += OnButtonSouth;
        _inputMap.UI.ButtonWest.canceled += OnButtonWest;
        _inputMap.UI.ButtonNorth.canceled += OnButtonNorth;
        _inputMap.UI.ButtonEast.canceled += OnButtonEast;
        _inputMap.UI.ShoulderR.canceled += OnShoulderR;
        _inputMap.UI.ShoulderL.canceled += OnShoulderL;
        _inputMap.UI.StartButton.canceled += OnStart;
    }
    private void UnsubscribeInputs()
    {
        _inputMap.UI.DPad.performed -= OnDpad;
        _inputMap.UI.ButtonSouth.performed -= OnButtonSouth;
        _inputMap.UI.ButtonWest.performed -= OnButtonWest;
        _inputMap.UI.ButtonNorth.performed -= OnButtonNorth;
        _inputMap.UI.ButtonEast.performed -= OnButtonEast;
        _inputMap.UI.ShoulderR.performed -= OnShoulderR;
        _inputMap.UI.ShoulderL.performed -= OnShoulderL;
        _inputMap.UI.StartButton.performed -= OnStart;
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
