﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChunkNameData))]
[RequireComponent(typeof(CoordinatesData))]

public class ChunkNameSetuper : MonoBehaviour
{
    public static event SecondaryChunkGenerating.GeneratingEvents GeneratingDone;

    private ChunkNameData chunkNameData;
    private CoordinatesData coordinatesData;

    private void Awake()
    {
        chunkNameData = GetComponent<ChunkNameData>();
        coordinatesData = GetComponent<CoordinatesData>();

        SetName();
    }

    public void SetName()
    {
        chunkNameData.value = "" + coordinatesData.x + "_" + coordinatesData.z;
    }
}
