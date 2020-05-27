using System.Collections;
using System.Collections.Generic;
using TopAdventure.Unity;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Actor), true)]
public class EntityListEditor : EditorOverride
{
    private Actor _actor;


    public void OnEnable()
    {
        _actor = (Actor) target;
    }

    public override void OnInspectorGUI()
    {
        if (_actor.behavioursList.Count <= 0)
            EditorGUILayout.LabelField("Please set behaviors");

        // if (_actor.behavioursList.Count > 0)
                    // {
                    //     for (int behaviourNumber = 0; behaviourNumber < _actor.behavioursList.Count; behaviourNumber++)
                    //     {
                    //         _actor.behavioursList[behaviourNumber] = (CustomBehaviour) EditorGUILayout.ObjectField(
                    //             $"{behaviourNumber}", _actor.behavioursList[behaviourNumber], typeof(CustomBehaviour),
                    //             false);
                    //     }
                    // }
        //
        // if (GUILayout.Button("Add Behaviour", GUILayout.Height(30)))
        //     _actor.behavioursList.Add(CreateInstance<CustomBehaviour>());


        base.OnInspectorGUI();
        // if (GUI.changed)
        //     SetObjectDirty(_actor.gameObject);
    }

    private static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}