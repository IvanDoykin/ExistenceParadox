using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//...maybe legacy :/
public class SomeStaff : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SaveChunk saveChunk = new SaveChunk();
            saveChunk.SaveAll();
        }

    }
}
