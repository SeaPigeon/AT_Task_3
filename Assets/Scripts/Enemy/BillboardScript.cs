using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    [SerializeField] private PlayerScript _player;
    [SerializeField] private Camera _camera;

    void Start()
    {
        SetUpReferences();
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = _camera.transform.rotation;
        transform.rotation = Quaternion.Euler(0f, _camera.transform.rotation.eulerAngles.y, 0f);
    }
    private void SetUpReferences()
    {
        _camera = PlayerScript.PlayerInstance.GetComponentInChildren<Camera>();
    }
    
}
