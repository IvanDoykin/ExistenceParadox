using UnityEngine;

public enum Directions 
{
    Left,
    Up,
    Right,
    Down,
    LeftUp,
    LeftDown,
    RightUp,
    RightDown
}

[RequireComponent(typeof(ChunksCoordination))]
public class ChunksCreator : MonoBehaviour
{
    private ChunksCoordination chunksCoordination;

    private void Start()
    {
        chunksCoordination = GetComponent<ChunksCoordination>();

        chunksCoordination.StarterCoordinating();
        StarterGenerating();
    }

    //Generating N x N chunks
    private void StarterGenerating()
    {
        CreateChunk(0, 0, gameObject.transform); //chunks[0,0] isn't includes in algoryth of generating
        CreateBlockOfChunks();
    }

    //creating chunks in special cycle
    private void CreateBlockOfChunks()
    {
        for (int round = 1; round < ChunksBlockData.halfChunkBlockSize + 1; round++)
        {
            int coordinateX = -round;
            int coordinateZ = -round;

            while (coordinateZ < round)
            {
                CreateChunk(coordinateX, coordinateZ, gameObject.transform);
                coordinateZ++;
            }

            while (coordinateX < round)
            {
                CreateChunk(coordinateX, coordinateZ, gameObject.transform);
                coordinateX++;
            }

            while (coordinateZ > -round)
            {
                CreateChunk(coordinateX, coordinateZ, gameObject.transform);
                coordinateZ--;
            }

            while (coordinateX > -round)
            {
                CreateChunk(coordinateX, coordinateZ, gameObject.transform);
                coordinateX--;
            }

        }
    }

    //Create chunk and put it in parent.gameObject
    public static void CreateChunk(int x, int z, Transform parent)
    {
        if (ChunksBlockData.chunks[x + ChunksBlockData.halfChunkBlockSize, z + ChunksBlockData.halfChunkBlockSize] != null)
            return;

        GameObject createdChunk = Instantiate(ChunksBlockData.chunk, new Vector3(ChunksCoordination.PopUpCoordinate(x + ChunksBlockData.zeroPointX), 0, ChunksCoordination.PopUpCoordinate(z + ChunksBlockData.zeroPointZ)), new Quaternion(0, 0, 0, 0), parent);

        createdChunk.GetComponent<ChunkData>().argX = x + ChunksBlockData.halfChunkBlockSize;
        createdChunk.GetComponent<ChunkData>().argZ = z + ChunksBlockData.halfChunkBlockSize;

        ChunksBlockData.chunks[x + ChunksBlockData.halfChunkBlockSize, z + ChunksBlockData.halfChunkBlockSize] = createdChunk.GetComponent<ChunkData>();
        createdChunk.GetComponent<ChunkData>().rangeOffset = Random.Range(-3,3); 
    }
}
