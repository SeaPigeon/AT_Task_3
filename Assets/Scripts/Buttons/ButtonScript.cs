using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonScript : MonoBehaviour
{
    private InputManagerScript _inputManager;
    private SceneManagerScript _sceneManager;
    
    private void Start()
    {
        SetUpReferences();
    }

    // Methods
    private void SetUpReferences()
    {
        _inputManager = FindObjectOfType<InputManagerScript>();
        _sceneManager = FindObjectOfType<SceneManagerScript>();
    }
    
    // Public Functions
    public void ActivateUIInputMap()
    {
        _inputManager.OnInputMapEvent(_inputManager.InputMap.UI);
        Debug.Log("smth1");
    }
    public void ActivateGameInputMap()
    {
        _inputManager.OnInputMapEvent(_inputManager.InputMap.Game);
        Debug.Log("smth2");
    }
    
    public void LoadDebugScene()
    {
        _sceneManager.OnLoadSceneEvent("DebugScene", 0);
    }
    public void LoadMainMenuScene()
    {
        _sceneManager.OnLoadSceneEvent("MainMenu", 1);
    }
    public void LoadControlMenuScene()
    {
        _sceneManager.OnLoadSceneEvent("ControlsMenu", 2);
    }
    public void LoadEditorScene()
    {
        _sceneManager.OnLoadSceneEvent("LevelEditor", 3);
    }
    public void LoadLevel1Scene()
    {
        _sceneManager.OnLoadSceneEvent("Level_1", 4);
    }
    
}
