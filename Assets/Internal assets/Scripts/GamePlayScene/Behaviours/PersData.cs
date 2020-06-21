using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PersData : Data
{
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public Rigidbody rigidbody;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;
    public float rotationX = 0;
}
