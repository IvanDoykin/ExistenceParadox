using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksUpdater : MonoBehaviour
{
    public static event Spaceman.SendChanging SendChange;

    public void ChunksUpdate(int offsetX, int offsetZ)
    {
        ChunksPriorityPlanner.SetNeedChunks(
            Mathf.Abs(offsetX) * ChunksBlockData.chunksBlockSize
            + Mathf.Abs(offsetZ) * ChunksBlockData.chunksBlockSize
            - Mathf.Abs(offsetX) * Mathf.Abs(offsetZ));

        ChangeZeroPoints(offsetX, offsetZ);
        ChunksMassiveUpdate(offsetX, offsetZ);
        ChunksFilling();
    }

    private void ChangeZeroPoints(int offsetX, int offsetZ)
    {
        ChunksBlockData.zeroPointX += offsetX;
        ChunksBlockData.zeroPointZ += offsetZ;
    }

    private void ChunksMassiveUpdate(int offsetX, int offsetZ)
    {
        ChunkData[,] buffer = new ChunkData[ChunksBlockData.chunksBlockSize, ChunksBlockData.chunksBlockSize];

        for (int i = 0; i < ChunksBlockData.chunksBlockSize; i++)
        {
            for (int j = 0; j < ChunksBlockData.chunksBlockSize; j++)
            {
                bool iIsOverflow = ((i - offsetX) >= ChunksBlockData.chunksBlockSize) || ((i - offsetX) < 0);
                bool jIsOverflow = ((j - offsetZ) >= ChunksBlockData.chunksBlockSize) || ((j - offsetZ) < 0);

                if (iIsOverflow || jIsOverflow)
                {
                    if (ChunksBlockData.chunks[i, j] != null)
                    {
                        Destroy(ChunksBlockData.chunks[i, j].gameObject); /////////////////////
                        ChunksBlockData.chunks[i, j] = null;
                    }
                }

                else
                {
                    buffer[i - offsetX, j - offsetZ] = ChunksBlockData.chunks[i, j];
                }
            }
        }

        for (int i = 0; i < ChunksBlockData.chunksBlockSize; i++)
        {
            for (int j = 0; j < ChunksBlockData.chunksBlockSize; j++)
            {
                ChunksBlockData.chunks[i, j] = buffer[i, j];
            }
        }
    }

    private void ChunksFilling()
    {
        for (int i = 0; i < ChunksBlockData.chunksBlockSize; i++)
        {
            for (int j = 0; j < ChunksBlockData.chunksBlockSize; j++)
            {
                if (ChunksBlockData.chunks[i, j] == null)
                {
                    ChunksCreator.CreateChunk(
                        i - ChunksBlockData.halfChunkBlockSize, 
                        j - ChunksBlockData.halfChunkBlockSize, 
                        gameObject.transform);
                }
            }
        }
    }

}
