using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChunkNameData))]
[RequireComponent(typeof(CoordinatingData))]

public class ChunkNameSetuper : MonoBehaviour
{
    public static event SecondaryChunkGenerating.GeneratingEvents GeneratingDone;

    private ChunkNameData chunkNameData;
    private CoordinatingData coordinatesData;

    private void Awake()
    {
        chunkNameData = GetComponent<ChunkNameData>();
        coordinatesData = GetComponent<CoordinatingData>();

        SetName();
    }

    public void SetName()
    {
        chunkNameData.value = "" + coordinatesData.x + "_" + coordinatesData.z;
    }
}
