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

[RequireComponent(typeof(ChunksBlockData))]
[RequireComponent(typeof(EdgeSmoother))]
public class ChunksBlock : MonoBehaviour
{
    ChunksBlockData chunksBlockData;

    public static event PrimaryChunkGenerating.CallChunkLinking NeedLink;
    public static event Spaceman.SendChanging SendChange;

    public delegate void ChunkAssembly();
    public static event ChunkAssembly AssemblyStart;

    private void Start()
    {
        chunksBlockData = GetComponent<ChunksBlockData>();

        StarterCoordinating();
        StarterGenerating();
    }

    public void LinkChunk(CoordinatesData coordinatesData)
    {
        int x = coordinatesData.x - chunksBlockData.zeroPointX;
        int z = coordinatesData.z - chunksBlockData.zeroPointZ;

        LinkChunk(x, z);
    }

    public void ChunksUpdate(int offsetX, int offsetZ)
    {
        //chunksAssembler.SetNeedGeneratedChunks(Mathf.Abs(offsetX) * ChunksBlockData.chunksBlockSize + Mathf.Abs(offsetZ) * ChunksBlockData.chunksBlockSize - Mathf.Abs(offsetX) * Mathf.Abs(offsetZ));

        ChangeZeroPoints(offsetX, offsetZ);
        ChunksMassiveUpdate(offsetX, offsetZ);
        ChunksFilling();
    }

    private void ChangeZeroPoints(int offsetX, int offsetZ)
    {
        chunksBlockData.zeroPointX += offsetX;
        chunksBlockData.zeroPointZ += offsetZ;
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
                        Destroy(ChunksBlockData.chunks[i, j].gameObject);
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
                    CreateChunk(i - ChunksBlockData.halfChunkBlockSize, j - ChunksBlockData.halfChunkBlockSize);
                }
            }
        }
    }

    private void StarterCoordinating()
    {
        chunksBlockData.zeroPointX = (int)SetUpCoordinate(transform.position.x) + ChunksBlockData.halfChunkBlockSize;
        chunksBlockData.zeroPointZ = (int)SetUpCoordinate(transform.position.z) + ChunksBlockData.halfChunkBlockSize;
    }

    private void StarterGenerating()
    {
        CreateChunk(0,0); //chunks[0,0] isn't includes in algoryth of generating
        CreateBlockOfChunks();
    }

    private void CreateBlockOfChunks()
    {
        for (int round = 1; round < ChunksBlockData.halfChunkBlockSize + 1; round++)
        {
            int coordinateX = -round;
            int coordinateZ = -round;

            while (coordinateZ < round)
            {
                CreateChunk(coordinateX, coordinateZ);
                coordinateZ++;
            }

            while (coordinateX < round)
            {
                CreateChunk(coordinateX, coordinateZ);
                coordinateX++;
            }

            while (coordinateZ > -round)
            {
                CreateChunk(coordinateX, coordinateZ);
                coordinateZ--;
            }

            while (coordinateX > -round)
            {
                CreateChunk(coordinateX, coordinateZ);
                coordinateX--;
            }

        }
    }

    private float SetUpCoordinate(float coordinate)
    {
        return (int)Mathf.Floor(coordinate / ChunkData.metric);
    }

    private float PopUpCoordinate(float coordinate)
    {
        return (coordinate * ChunkData.metric);
    }

    private void CreateChunk(int x, int z)
    {
        if (ChunksBlockData.chunks[x + ChunksBlockData.halfChunkBlockSize, z + ChunksBlockData.halfChunkBlockSize] != null)
            return;

        GameObject createdChunk = Instantiate(chunksBlockData.chunk, new Vector3(PopUpCoordinate(x + chunksBlockData.zeroPointX), 0, PopUpCoordinate(z + chunksBlockData.zeroPointZ)), new Quaternion(0, 0, 0, 0), gameObject.transform);

        createdChunk.GetComponent<ChunkData>().argX = x + ChunksBlockData.halfChunkBlockSize;
        createdChunk.GetComponent<ChunkData>().argZ = z + ChunksBlockData.halfChunkBlockSize;

        ChunksBlockData.chunks[x + ChunksBlockData.halfChunkBlockSize, z + ChunksBlockData.halfChunkBlockSize] = createdChunk.GetComponent<ChunkData>();
        createdChunk.GetComponent<ChunkData>().rangeOffset = Random.Range(-3,3); 
    }

    private void LinkChunk(int x, int z)
    {
        bool right = x != ChunksBlockData.halfChunkBlockSize;
        bool left = x != -ChunksBlockData.halfChunkBlockSize;
        bool up = z != ChunksBlockData.halfChunkBlockSize;
        bool down = z != -ChunksBlockData.halfChunkBlockSize;

        if (right)
           EqualEdgeDots(x, z, Directions.Right);

        if (left)
           EqualEdgeDots(x, z, Directions.Left);

        if (down)
           EqualEdgeDots(x, z, Directions.Down);

        if (up)
           EqualEdgeDots(x, z, Directions.Up);

        if (right && down)
           EqualEdgeDots(x, z, Directions.RightDown);

        if (right && up)
           EqualEdgeDots(x, z, Directions.RightUp);

        if (left && down)
           EqualEdgeDots(x, z, Directions.LeftDown);

        if (left && up)
           EqualEdgeDots(x, z, Directions.LeftUp);
    }

    private void EqualEdgeDots(int x, int z, Directions direction)
    {
        int indexAdditionX = 0;
        int indexAdditionZ = 0;
        int startPoint = 0;
        int step = 0;
        int otherChunkDot = 0;
        int offsetDown = 0;

        switch (direction)
        {
            case Directions.Right:
                {
                    indexAdditionX = 1;
                    indexAdditionZ = 0;
                    startPoint = ChunkData.size;
                    step = ChunkData.size + 1;
                    otherChunkDot = -ChunkData.size;
                    break;
                }

            case Directions.Left:
                {
                    indexAdditionX = -1;
                    indexAdditionZ = 0;
                    startPoint = 0;
                    step = ChunkData.size + 1;
                    otherChunkDot = ChunkData.size;
                    break;
                }

            case Directions.Up:
                {
                    indexAdditionX = 0;
                    indexAdditionZ = 1;
                    startPoint = ChunkData.size * (ChunkData.size + 1);
                    step = 1;
                    otherChunkDot = -startPoint;
                    break;
                }

            case Directions.Down:
                {
                    indexAdditionX = 0;
                    indexAdditionZ = -1;
                    startPoint = 0;
                    step = 1;
                    otherChunkDot = ChunkData.size * (ChunkData.size + 1);
                    offsetDown = -otherChunkDot;
                    break;
                }

            case Directions.RightUp:
                {
                    indexAdditionX = 1;
                    indexAdditionZ = 1;
                    startPoint = (ChunkData.size + 1) * (ChunkData.size + 1) - 1;
                    step = startPoint + 1;
                    otherChunkDot = -startPoint;
                    break;
                }

            case Directions.LeftUp:
                {
                    indexAdditionX = -1;
                    indexAdditionZ = 1;
                    startPoint = ChunkData.size * (ChunkData.size + 1);
                    step = startPoint + ChunkData.size + 1;
                    otherChunkDot = ChunkData.size - startPoint;
                    break;
                }

            case Directions.RightDown:
                {
                    indexAdditionX = 1;
                    indexAdditionZ = -1;
                    startPoint = ChunkData.size;
                    step = (ChunkData.size + 1) * (ChunkData.size + 1) + 1;
                    otherChunkDot = ChunkData.size * ChunkData.size;
                    break;
                }

            case Directions.LeftDown:
                {
                    indexAdditionX = -1;
                    indexAdditionZ = -1;
                    startPoint = 0;
                    step = (ChunkData.size + 1) * (ChunkData.size + 1) + 1;
                    otherChunkDot = step - 2;
                    offsetDown = -ChunkData.size * (ChunkData.size + 1);
                    break;
                }
        }

        ChunkData parentChunk = ChunksBlockData.chunks[x + indexAdditionX + ChunksBlockData.halfChunkBlockSize, z + indexAdditionZ + ChunksBlockData.halfChunkBlockSize];
        ChunkData childChunk = ChunksBlockData.chunks[x + ChunksBlockData.halfChunkBlockSize, z + ChunksBlockData.halfChunkBlockSize];

        if ((parentChunk != null) && (parentChunk.constructed == true))
        {
            int dotsLength = (ChunkData.size + 1) * (ChunkData.size + 1);

            for (int i = startPoint; i < dotsLength + offsetDown; i += step)
            {
                childChunk.dots[i].y = parentChunk.dots[i + otherChunkDot].y;

                childChunk.notCalculatedVecs[i] = 0;
            }
        }
    }
}
