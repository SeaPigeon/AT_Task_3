using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    private InputManagerScript _inputManager;
    private SceneManagerScript _sceneManager;
    private UIManagerScript _UIManager;
    private PlayerScript _player;
    
    private void Start()
    {
        SetUpReferences();
    }

    // Methods
    private void SetUpReferences()
    {
        _inputManager = FindObjectOfType<InputManagerScript>();
        _sceneManager = FindObjectOfType<SceneManagerScript>();
        _UIManager = FindObjectOfType<UIManagerScript>();
        _player = FindObjectOfType<PlayerScript>();
    }
    
    // Public Functions
    public void ActivateUIInputMap()
    {
        _inputManager.OnInputMapEvent(_inputManager.InputMap.UI);
    }
    public void ActivateGameInputMap()
    {
        _inputManager.OnInputMapEvent(_inputManager.InputMap.Game);
    }
    
    public void LoadDebugScene()
    {
        _sceneManager.OnLoadScene("DebugScene");
        _UIManager.LoadCanvas(0);
    }
    public void LoadMainMenuScene()
    {
        _sceneManager.OnLoadScene("MainMenu");
        _UIManager.LoadCanvas(1);
    }
    public void LoadControlMenuScene()
    {
        _sceneManager.OnLoadScene("ControlsMenu");
        _UIManager.LoadCanvas(2);
    }
    public void LoadEditorScene()
    {
        _sceneManager.OnLoadScene("LevelEditor");
        _UIManager.LoadCanvas(3);
    }
    public void LoadLevel1Scene()
    {
        _sceneManager.OnLoadScene("Level_1");
        _UIManager.LoadCanvas(4);
    }

}
