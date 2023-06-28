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
    
    private void Start()
    {
        SetUpReferences();
    }

    // Methods
    private void SetUpReferences()
    {
        _inputManager = InputManagerScript.IMInstance.GetComponent<InputManagerScript>();
        _sceneManager = SceneManagerScript.SMInstance.GetComponent<SceneManagerScript>();
        _UIManager = UIManagerScript.UIMInstance.GetComponent<UIManagerScript>();
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
        //PlayerScript.PlayerInstance.TogglePlayerSprite(false);
    }
    public void LoadMainMenuScene()
    {
        _sceneManager.OnLoadScene("MainMenu");
        _UIManager.LoadCanvas(1);
        //PlayerScript.PlayerInstance.TogglePlayerSprite(false);
    }
    public void LoadControlMenuScene()
    {
        _sceneManager.OnLoadScene("ControlsMenu");
        _UIManager.LoadCanvas(2);
        //PlayerScript.PlayerInstance.TogglePlayerSprite(false);
    }
    public void LoadEditorScene()
    {
        _sceneManager.OnLoadScene("LevelEditor");
        _UIManager.LoadCanvas(3);
        //PlayerScript.PlayerInstance.TogglePlayerSprite(false);
    }
    public void LoadLevel1Scene()
    {
        _sceneManager.OnLoadScene("Level_1");
        _UIManager.LoadCanvas(4);
        //PlayerScript.PlayerInstance.SpawnPlayer();
    }
    public void LoadLevel2Scene()
    {
        _sceneManager.OnLoadScene("Level_2");
        _UIManager.LoadCanvas(4);
        //PlayerScript.PlayerInstance.MoveToSpawnPoint();
    }
    public void LoadLevel3Scene()
    {
        _sceneManager.OnLoadScene("Level_3");
        _UIManager.LoadCanvas(4);
        //PlayerScript.PlayerInstance.MoveToSpawnPoint();
    }
}
