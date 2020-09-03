using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
