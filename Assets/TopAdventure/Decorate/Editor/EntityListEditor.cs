using System.Collections;
using System.Collections.Generic;
using TopAdventure.Unity;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Entity), true)]
public class EntityListEditor : EditorOverride
{
    private Entity _entity;


    public void OnEnable()
    {
        _entity = (Entity) target;
    }

    public override void OnInspectorGUI()
    {
        if (_entity.behavioursList.Count <= 0)
            EditorGUILayout.LabelField("Please set behaviors");

        // if (_entity.behavioursList.Count > 0)
                    // {
                    //     for (int behaviourNumber = 0; behaviourNumber < _entity.behavioursList.Count; behaviourNumber++)
                    //     {
                    //         _entity.behavioursList[behaviourNumber] = (CustomBehaviour) EditorGUILayout.ObjectField(
                    //             $"{behaviourNumber}", _entity.behavioursList[behaviourNumber], typeof(CustomBehaviour),
                    //             false);
                    //     }
                    // }
        //
        // if (GUILayout.Button("Add Behaviour", GUILayout.Height(30)))
        //     _entity.behavioursList.Add(CreateInstance<CustomBehaviour>());


        base.OnInspectorGUI();
        // if (GUI.changed)
        //     SetObjectDirty(_entity.gameObject);
    }

    private static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}