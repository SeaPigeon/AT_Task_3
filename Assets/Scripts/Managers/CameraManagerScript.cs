using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManagerScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _activeCamera;
    [SerializeField] List<CinemachineVirtualCamera> _cameraList;

    [SerializeField] GameManagerScript _gameManager;
    [SerializeField] PlayerScript _player;
    
    private static CameraManagerScript _CameraManagerInstance = null;
    // G&S
    public static CameraManagerScript CMInstance { get {return _CameraManagerInstance; } }
    public CinemachineVirtualCamera ActiveCamera {get { return _activeCamera; } set { _activeCamera = value; } }
    public List<CinemachineVirtualCamera> CamerasList { get { return _cameraList; } }
    private void Awake()
    {
        CameraManagerSingleton();   
    }

    private void Start()
    {
        SetUpReferences();
        SetUpEvents();
        AddCamerasToList();
        
    }

    private void CameraManagerSingleton()
    {
        if (_CameraManagerInstance == null)
        {
            _CameraManagerInstance = this;
        }
        else if (_CameraManagerInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    private void SetUpReferences()
    {
        _gameManager = GameManagerScript.GMInstance;
        _player = PlayerScript.PlayerInstance;
    }
    private void SetUpEvents()
    {
        //_gameManager.OnGMSetUpComplete += SetUpCameraManager;
        _gameManager.OnGMSetUpComplete += Test;
    }
    
    private void AddCamerasToList()
    {
        _cameraList.Add(gameObject.GetComponentInChildren<CinemachineVirtualCamera>());
        _cameraList.Add(_player.InGameCamera);
    }
    private void Test()
    {
        if (_gameManager.SceneLoadedIndex == 4)
        {
            _activeCamera = _cameraList[1];
        }
        else
        {
            _activeCamera = _cameraList[0];
        }
        
        _gameManager.ActiveCamera = _activeCamera; 
        Debug.Log("CameraManager Call from GMEvent: " + _activeCamera);
    }
    /*public void SetUpCameraManager()
    {
        AddCamerasToList();
        if (_gameManager.SceneLoadedIndex == 4 ||
            _gameManager.SceneLoadedIndex == 5 ||
            _gameManager.SceneLoadedIndex == 6)
        {
            ActivateCameraCinemachine(1);
            Debug.Log("Game Cam Active");
        }
        else
        {
            ActivateCameraCinemachine(0);
            Debug.Log("Menu Cam Active");
        }

        Debug.Log("CameraManager CallFromGMEvent");
    }*/

    public void ActivateCameraCinemachine(int index)
    {
        /*if (ActiveCamera != null)
        {
            ActiveCamera.Priority = 0;
        }*/
        //_cameraList[index].Priority = 10;
        _activeCamera = _cameraList[index];
        //_gameManager.ActiveCamera = _cameraList[index];
        Debug.Log("camera call");
    }
}
