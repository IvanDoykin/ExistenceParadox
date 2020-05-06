using UnityEngine;

public class ChunksControllerData : MonoBehaviour
{
    public GameObject chunk;

    public const int chunksBlockSize = 11; //not even number
    public const int halfChunkBlockSize = (chunksBlockSize - 1) / 2;

    public int zeroPointX;
    public int zeroPointZ;

    public static ChunkData[,] chunks = new ChunkData[chunksBlockSize, chunksBlockSize];
}
