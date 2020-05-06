using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{    
    public KeyCode[] codes;
    private bool _stateCursor = false;  //false - cursor enabled, true - disabled

    private void Start()
    {
        ChangeStateCursor();        
    }
    void Update()   //TODO fix this shit
    {
        for(int i = 0; i < codes.Length; i++)
        {
            if (Input.GetKeyUp(codes[i]))
            {            
                ChangeStateCursor();
            }
        }
    }

    private void ChangeStateCursor()
    {
        if (_stateCursor)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        Cursor.visible = _stateCursor;
        GetComponent<CameraControlV2>().enabled = !_stateCursor;
        _stateCursor = !_stateCursor;
    }
}
