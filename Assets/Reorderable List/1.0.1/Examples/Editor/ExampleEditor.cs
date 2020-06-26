using UnityEngine;
using UnityEditor;
using System.Collections;
using Malee.List;
using System;
using System.Linq;

[CanEditMultipleObjects]
[CustomEditor(typeof(Example))]
public class ExampleEditor : Editor
{
    private ReorderableList list1;
    private SerializedProperty list2;
    private ReorderableList list3;
    private ReorderableList customList;
    private Example _example;

    void OnEnable()
    {
        list1 = new ReorderableList(serializedObject.FindProperty("list1"));
        list1.elementNameProperty = "myEnum";

        customList = new ReorderableList(serializedObject.FindProperty("customList"));
        customList.onAddCallback += SetDefaultValueForCustomBehaviourList;
            
        list2 = serializedObject.FindProperty("list2");

        list3 = new ReorderableList(serializedObject.FindProperty("list3"));
        list3.getElementNameCallback += GetList3ElementName;
    }

    private string GetList3ElementName(SerializedProperty element)
    {
        return element.propertyPath;
    }

    private void SetDefaultValueForCustomBehaviourList(ReorderableList list)
    {
        customList.AddItem(ScriptableObject.CreateInstance<DefaultValueBehaviour>());
    }

    public override void OnInspectorGUI()
    {
    
        serializedObject.Update();

        //draw the list using GUILayout, you can of course specify your own position and label
        list1.DoLayoutList();
        customList.DoLayoutList();

        //Caching the property is recommended
        EditorGUILayout.PropertyField(list2);

        //draw the final list, the element name is supplied through the callback defined above "GetList3ElementName"
        list3.DoLayoutList();


        //Draw without caching property
        EditorGUILayout.PropertyField(serializedObject.FindProperty("list4"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("list5"));

        serializedObject.ApplyModifiedProperties();
    }
}