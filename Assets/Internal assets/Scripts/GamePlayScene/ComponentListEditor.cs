using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Actor))]
public class ComponentListEditor : Editor
{
    private Actor _actor;

    public void OnEnable()
    {
        _actor = (Actor) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
       
    }
}