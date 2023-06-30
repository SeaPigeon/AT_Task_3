using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class BillboardScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private CameraManagerScript _cameraManager;

    void Start()
    {
        SetUpReferences();
    }

    void LateUpdate()
    {
        Rotate();
    }
    private void SetUpReferences()
    {
        _cameraManager = CameraManagerScript.CMInstance;
        _camera = _cameraManager.CamerasList[1];
    }
    private void Rotate()
    {
        transform.rotation = PlayerScript.PlayerInstance.transform.rotation;
    }

}
