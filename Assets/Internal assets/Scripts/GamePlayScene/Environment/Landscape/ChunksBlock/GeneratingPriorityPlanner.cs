using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratingPriorityPlanner : MonoBehaviour
{
    private static int needChunks;
    private static int currentChunks;

    public delegate void PriorityEvent();
    public static event PriorityEvent CallChunkGenerating;

    private void Start()
    {
        currentChunks = 0;
        needChunks = ChunksBlockData.chunksBlockSize * ChunksBlockData.chunksBlockSize;
    }

    public void SetNeedChunks(int value)
    {
        if (currentChunks > 0)
            return;

        if (value > 0)
            needChunks = value;
    }

    public static void NotifyChunk()
    {
        currentChunks++;

        if(currentChunks >= needChunks)
        {
            Debug.Log("Noticed chunks: " + currentChunks);
            currentChunks = 0;
            SomeForFun();
        }
    }

    private static void SomeForFun()
    {

        foreach (ChunkData chunk in ChunksBlockData.chunks)
        {
            if (!chunk.loaded)
            {
                CallChunkGenerating += chunk.GetComponent<ChunkGenerator>().SomeBuddy;
                CallChunkGenerating();
                CallChunkGenerating -= chunk.GetComponent<ChunkGenerator>().SomeBuddy;
            }
        }
    }
}
