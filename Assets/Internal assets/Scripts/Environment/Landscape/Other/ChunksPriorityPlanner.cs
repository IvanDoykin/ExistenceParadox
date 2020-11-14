using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ChunksPriorityPlanner : MonoBehaviour
{
    [SerializeField] private static List<ChunkGenerator> chunksGenerators;
    private static int needChunks = ChunksBlockData.chunksBlockSize * ChunksBlockData.chunksBlockSize;

    private void Start()
    {
        chunksGenerators = new List<ChunkGenerator>();
    }

    public static void SetNeedChunks(int value)
    {
        if (value > 0)
            needChunks = value;
    }

    public static void AddChunk(ChunkGenerator chunkGenerator)
    {
        chunksGenerators.Add(chunkGenerator);

        if (chunksGenerators.Count >= needChunks)
        {
            GenerateAllChunks();
            chunksGenerators.Clear();
        }
    }

    private static void GenerateAllChunks()
    {
        var sortedChunksGenerators = from chunk in chunksGenerators
                                     orderby chunk.loaded descending
                                     select chunk;

        Debug.Log("");
        Debug.Log("Start generating");
        Debug.Log("");

        foreach (var chunk in sortedChunksGenerators)
        {
            Debug.Log("Chunk from HDD?: " + chunk.loaded);
            chunk.Generate();
        }

        Debug.Log("");
        Debug.Log("Generating done");
        Debug.Log("");
    }
}
