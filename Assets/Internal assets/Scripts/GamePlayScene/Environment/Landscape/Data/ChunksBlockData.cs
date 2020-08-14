using UnityEngine;

public class ChunksBlockData : MonoBehaviour
{
    [SerializeField] private GameObject Chunk;
    public static GameObject chunk; //chunk's prefab

    private void Awake()
    {
        chunk = Chunk;
    }

    public const int chunksBlockSize = 13; //(NOT EVEN NUMBER!!!) It set chunks block's size (N x N)
    public const int halfChunkBlockSize = (chunksBlockSize - 1) / 2; //for convinient calculations

    public static int zeroPointX;
    public static int zeroPointZ;

    public static ChunkData[,] chunks = new ChunkData[chunksBlockSize, chunksBlockSize]; //keep all visible chunks
}
