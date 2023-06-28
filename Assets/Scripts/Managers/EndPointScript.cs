using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Scenes
{
    Debug,
    MainMenu,
    ControlsMenu,
    Editor,
    Level_1,
    Level_2,
    Level_3
}
public class EndPointScript : MonoBehaviour
{
    [SerializeField] private Scenes _sceneToLoad;
    private SceneManagerScript _sceneManager;
    private UIManagerScript _UIManager;
    

    void Start()
    {
        _sceneManager = SceneManagerScript.SMInstance.GetComponent<SceneManagerScript>();
        _UIManager = UIManagerScript.UIMInstance.GetComponent<UIManagerScript>();
    }

    private void LoadScene()
    {
        switch (_sceneToLoad)
        {
            case Scenes.Debug:
                LoadDebugScene();
                break;

            case Scenes.MainMenu:
                LoadMainMenuScene();
                break;

            case Scenes.ControlsMenu:
                LoadControlMenuScene();
                break;

            case Scenes.Editor:
                LoadEditorScene();
                break;

            case Scenes.Level_1:
                LoadLevel1Scene();
                break;

            case Scenes.Level_2:
                LoadLevel2Scene();
                break;

            case Scenes.Level_3:
                LoadLevel3Scene();
                break;
            default:
                break;
        }
    }
    public void LoadDebugScene()
    {
        _sceneManager.OnLoadScene("DebugScene");
        _UIManager.LoadCanvas(0);
        //PlayerScript.PlayerInstance.TogglePlayerSprite(false); ;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        {
            LoadScene();
        }
    }
}
