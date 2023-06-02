using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] InputManagerScript _inputManager;

    private void Awake() 
    {
        _inputManager = FindObjectOfType<InputManagerScript>();
        SetUpInputManager();
    }

    private void SetUpInputManager()
    {
        
    }
}
