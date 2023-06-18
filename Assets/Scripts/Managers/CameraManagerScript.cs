using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManagerScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _activeCamera;
    [SerializeField] CinemachineVirtualCamera[] _cameraList;

    [SerializeField] GameManagerScript _gameManager;
    // G&S
    public CinemachineVirtualCamera ActiveCamera {get { return _activeCamera; } set { _activeCamera = value; } }

    public void ActivateCamera(int index)
    {
        //ActiveCamera.SetActive(false);
        //ActivateCamera = _cameraList[index].SetActive(true);
        //_gameManager.ActivateCamera = ActivateCamera;
    }
}
