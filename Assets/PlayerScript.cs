using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] int _MAX_HEALTH = 100;
    [SerializeField] int _currentHealth;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _runSpeed;
    [SerializeField] float _fireDelay;
    [SerializeField] string _sceneToLoadOnDeath;

    [Header("Debug")]
    [SerializeField] GameManagerScript _gameManager;
    [SerializeField] SceneManagerScript _sceneManager;
    [SerializeField] InputManagerScript _inputManager;

    private void Awake() 
    {
        _gameManager = FindObjectOfType<GameManagerScript>();
        _inputManager = FindObjectOfType<InputManagerScript>();
        _sceneManager = FindObjectOfType<SceneManagerScript>();
        //SetUpInputManager();
        //SetUpPlayer();
    }

    // G&S
    public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }

    private void SetUpInputManager()
    {
        /*_inputMap = new InputMap();
        _inputMap.Game.Enable();

        _inputMap.Game.Move.performed += OnMove;
        _inputMap.Game.ButtonSouth.performed += OnButtonSouth;
        _inputMap.Game.ButtonWest.performed += OnButtonWest;
        _inputMap.Game.ButtonNorth.performed += OnButtonNorth;
        _inputMap.Game.ButtonEast.performed += OnButtonEast;
        _inputMap.Game.ShoulderR.performed += OnShoulderR;
        _inputMap.Game.ShoulderL.performed += OnShoulderL;
        _inputMap.Game.StartButton.performed += OnStart;

        _inputMap.Game.Move.canceled += OnMove;
        _inputMap.Game.ButtonSouth.canceled += OnButtonSouth;
        _inputMap.Game.ButtonWest.canceled += OnButtonWest;
        _inputMap.Game.ButtonNorth.canceled += OnButtonNorth;
        _inputMap.Game.ButtonEast.canceled += OnButtonEast;
        _inputMap.Game.ShoulderR.canceled += OnShoulderR;
        _inputMap.Game.ShoulderL.canceled += OnShoulderL;
        _inputMap.Game.StartButton.canceled += OnStart;*/
    }

    private void UnsubscribeInputs()
    {
        /*_inputMap.Game.DPad.performed -= OnDpad;
        _inputMap.Game.ButtonSouth.performed -= OnButtonSouth;
        _inputMap.Game.ButtonWest.performed -= OnButtonWest;
        _inputMap.Game.ButtonNorth.performed -= OnButtonNorth;
        _inputMap.Game.ButtonEast.performed -= OnButtonEast;
        _inputMap.Game.ShoulderR.performed -= OnShoulderR;
        _inputMap.Game.ShoulderL.performed -= OnShoulderL;
        _inputMap.Game.StartButton.performed -= OnStart;*/
    }

/*
    private void OnMove(InputAction.CallbackContext context) 
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

    private void SetUpPlayer()
    {
        CurrentHealth = _MAX_HEALTH;
        _gameManager.ResetScore();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        
        if (CurrentHealth <= 0)
        {
            _sceneManager.LoadScene(_sceneToLoadOnDeath);
        }
    }
*/
}
