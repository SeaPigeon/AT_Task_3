using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = FindObjectOfType<Camera>();        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = _camera.transform.rotation;
        transform.rotation = Quaternion.Euler(0f, _camera.transform.rotation.eulerAngles.y, 0f);


    }
}
