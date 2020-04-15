using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChunkNameData))]
[RequireComponent(typeof(CoordinatesData))]

public class ChunkNameSetuper : MonoBehaviour
{
    public static event ChunkGenerating.GeneratingEvents GeneratingDone;

    private ChunkNameData chunkNameData;
    private CoordinatesData coordinatesData;

    private void Awake()
    {
        chunkNameData = GetComponent<ChunkNameData>();
        coordinatesData = GetComponent<CoordinatesData>();
    }

    public void SetName()
    {
        chunkNameData.value = "" + coordinatesData.x + ":" + coordinatesData.z + ".cached";
    }
}
