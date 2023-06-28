using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagerScript : MonoBehaviour
{
    [SerializeField] Camera _activeCamera;
    [SerializeField] List<Camera> _cameraList = new List<Camera>();

    [SerializeField] GameManagerScript _gameManager;
    
    private static CameraManagerScript _CameraManagerInstance = null;
    // G&S
    public static CameraManagerScript CMInstance { get {return _CameraManagerInstance; } }
    public Camera ActiveCamera {get { return _activeCamera; } set { _activeCamera = value; } }
    private void Awake()
    {
        CameraManagerSingleton();   
    }

    private void Start()
    {
        SetUpCameraManager();
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
    private void SetUpCameraManager()
    {
        _cameraList.AddRange(FindObjectsOfType<Camera>());
        ActivateCamera(0);
    }

    public void ActivateCamera(int index)
    {
        Debug.Log("Call");
        if (ActiveCamera != null)
        {
            ActiveCamera.enabled = false;
        }
        ActiveCamera = _cameraList[index];
        Debug.Log(_cameraList[index].name);
        ActiveCamera.enabled = true;
        //_gameManager.ActiveCamera = ActiveCamera;
    }
}
