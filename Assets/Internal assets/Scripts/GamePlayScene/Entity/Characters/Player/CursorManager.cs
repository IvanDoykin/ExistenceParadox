﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{    
    public KeyCode[] codes;
    private bool _stateCursor = false;  //false - cursor enabled, true - disabled

    private void Start()
    {
        ChangeStateCursor();        
    } // create empty line - one style for code
    void Update()   //TODO fix this shit
    {
        for(int i = 0; i < codes.Length; i++) //NEVER include 'for' in update
        {
            if (Input.GetKeyUp(codes[i]))
            {            
                ChangeStateCursor(); //please - DELETE WHOLE UPDATE and better - whole script
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
//useless script, sorry :(