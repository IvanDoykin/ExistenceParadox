using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Directions 
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

public class ChunksController : MonoBehaviour
{
    [SerializeField] GameObject chunk;

    private const int chunkMetric = 16;
    private const int chunksBlockSize = 11; //not even number
    private const int halfChunkBlockSize = (chunksBlockSize - 1) / 2;

    private int zeroPointX;
    private int zeroPointZ;
    private static ChunkData[,] chunks = new ChunkData[chunksBlockSize, chunksBlockSize];

    public static event ChunkGenerating.CallChunkLinking NeedLink;

    private void Start()
    {
        zeroPointX = (int)SetUpCoordinate(transform.position.x) + halfChunkBlockSize;
        zeroPointZ = (int)SetUpCoordinate(transform.position.z) + halfChunkBlockSize;

        StarterGenerating();
    }

    public void Test(CoordinatesData coordinatesData)
    {
        int x = coordinatesData.x - zeroPointX;
        int z = coordinatesData.z - zeroPointZ;
        LinkOneChunk(x, z);
    }

    public void ChangeZeroPoints()
    {
        //for event when player goes on the other chunk
    }

    private void StarterGenerating()
    {
        CreateChunk(0,0); //chunks[0,0] isn't includes in algoryth of generating
        CreateBlockOfChunks();
        
        Debug.Log("===================ALL DONE ===================");
    }

    private void CreateBlockOfChunks()
    {
        for (int round = 1; round < halfChunkBlockSize + 1; round++)
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
        return (int)Mathf.Floor(coordinate / 16);
    }

    private float PopUpCoordinate(float coordinate)
    {
        return (coordinate * 16);
    }

    private void CreateChunk(int x, int z)
    {
        GameObject createdChunk = Instantiate(chunk, new Vector3(PopUpCoordinate(x + zeroPointX), 0, PopUpCoordinate(z + zeroPointZ)), new Quaternion(0, 0, 0, 0), gameObject.transform);
        chunks[x + halfChunkBlockSize, z + halfChunkBlockSize] = createdChunk.GetComponent<ChunkData>();
    }

    private void LinkChunks(int x, int z)
    {
        for (int round = 1; round < halfChunkBlockSize + 1; round++)
        {
            int coordinateX = x - round;
            int coordinateZ = z - round;

            while (coordinateZ < z + round)
            {
                LinkOneChunk(coordinateX, coordinateZ);
                coordinateZ++;
            }

            while (coordinateX < x + round)
            {
                LinkOneChunk(coordinateX, coordinateZ);
                coordinateX++;
            }

            while (coordinateZ > z - round)
            {
                LinkOneChunk(coordinateX, coordinateZ);
                coordinateZ--;
            }

            while (coordinateX > x - round)
            {
                LinkOneChunk(coordinateX, coordinateZ);
                coordinateX--;
            }

        }
    }

    private void LinkOneChunk(int x, int z) //x,z Э [-5;5]
    {
        bool right = x != halfChunkBlockSize;
        bool left = x != -halfChunkBlockSize;
        bool up = z != halfChunkBlockSize;
        bool down = z != -halfChunkBlockSize;

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

        if ((chunks[x + indexAdditionX + halfChunkBlockSize, z + indexAdditionZ + halfChunkBlockSize] != null) && (!chunks[x + indexAdditionX + halfChunkBlockSize, z + indexAdditionZ + halfChunkBlockSize].constructed))
            Debug.Log("============================FALSE=================");

        if ((chunks[x + indexAdditionX + halfChunkBlockSize, z + indexAdditionZ + halfChunkBlockSize] != null) &&
          (chunks[x + indexAdditionX + halfChunkBlockSize, z + indexAdditionZ + halfChunkBlockSize].constructed == true))
        {
            Debug.Log("SAD BUT TRUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUE");
            Debug.Log("x = " + x);
            Debug.Log("z = " + z);
            Debug.Log(direction);
            int dotsLength = (ChunkData.size + 1) * (ChunkData.size + 1);

            for (int i = startPoint; i < dotsLength + offsetDown; i += step)
            {
                if ((i + otherChunkDot) >= chunks[0,0].dots.Length)
                    Debug.Log("---------------------------------");
                chunks[x + halfChunkBlockSize, z + halfChunkBlockSize].dots[i].y = chunks[x + indexAdditionX + halfChunkBlockSize, z + indexAdditionZ + halfChunkBlockSize].dots[i + otherChunkDot].y;
                chunks[x + halfChunkBlockSize, z + halfChunkBlockSize].notCalculatedVecs[i] = 0;
            }
        }

    }
}
