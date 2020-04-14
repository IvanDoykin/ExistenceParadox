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

[RequireComponent(typeof(CoordinatesData))]

public class ChunksController : MonoBehaviour
{
    [SerializeField] GameObject chunk;

    private const int chunkMetric = 16;
    private const int chunksBlockSize = 11; //not even number
    private const int halfChunkBlockSize = (chunksBlockSize - 1) / 2;

    private CoordinatesData coordinates;
    private int zeroPointX;
    private int zeroPointZ;
    private ChunkData[,] chunks = new ChunkData[chunksBlockSize, chunksBlockSize];

    private void Start()
    {
        coordinates = GetComponent<CoordinatesData>();

        zeroPointX = coordinates.x - halfChunkBlockSize;
        zeroPointZ = coordinates.z - halfChunkBlockSize;

        StarterGenerating();
    }

    public void ChangeZeroPoints()
    {
        //for event when player goes on the other chunk
    }

    private void StarterGenerating()
    {
        int x = coordinates.x;
        int z = coordinates.z;

        CreateChunk(x, z);

        for (int round = 1; round < halfChunkBlockSize + 1; round++)
        {
            int coordinateX = x - round;
            int coordinateZ = z - round;

            while (coordinateZ < z + round)
            {
                CreateChunk(coordinateX, coordinateZ);
                coordinateZ++;
            }

            while (coordinateX < x + round)
            {
                CreateChunk(coordinateX, coordinateZ);
                coordinateX++;
            }

            while (coordinateZ > z - round)
            {
                CreateChunk(coordinateX, coordinateZ);
                coordinateZ--;
            }

            while (coordinateX > x - round - 1)
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
        GameObject createdChunk = Instantiate(chunk, new Vector3(PopUpCoordinate(x - zeroPointX), 0, PopUpCoordinate(z - zeroPointZ)), new Quaternion(0, 0, 0, 0), gameObject.transform);
        chunks[x - zeroPointX, z - zeroPointZ] = createdChunk.GetComponent<ChunkData>();
        LinkChunk(x - zeroPointX, z - zeroPointZ);
    }

    private void LinkChunk(int x, int z)
    {
        if ((x + 1) % chunksBlockSize != 0) 
            EqualEdgeDots(x, z, Directions.Right);

        if (x % chunksBlockSize != 0)
            EqualEdgeDots(x, z, Directions.Left);

        if (z > 0)
            EqualEdgeDots(x, z, Directions.Down);

        if (z < chunksBlockSize)
            EqualEdgeDots(x, z, Directions.Up);

        if (((x + 1) % chunksBlockSize != 0) && (z > 0))
            EqualEdgeDots(x, z, Directions.RightDown);

        if (((x + 1) % chunksBlockSize != 0) && (z < chunksBlockSize))
            EqualEdgeDots(x, z, Directions.RightUp);

        if ((x % chunksBlockSize != 0) && (z > 0))
            EqualEdgeDots(x, z, Directions.LeftDown);

        if ((x % chunksBlockSize != 0) && (z < chunksBlockSize))
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
                    startPoint = ChunkData.size * ChunkData.size - 1;
                    step = startPoint + 1;
                    otherChunkDot = -startPoint;
                    break;
                }

            case Directions.LeftUp:
                {
                    indexAdditionX = -1;
                    indexAdditionZ = 1;
                    startPoint = ChunkData.size * (ChunkData.size + 1);
                    step = startPoint + 1;
                    otherChunkDot = ChunkData.size - startPoint;
                    break;
                }

            case Directions.RightDown:
                {
                    indexAdditionX = 1;
                    indexAdditionZ = -1;
                    startPoint = ChunkData.size;
                    step = (ChunkData.size + 1) * (ChunkData.size + 1);
                    otherChunkDot = ChunkData.size * ChunkData.size;
                    break;
                }

            case Directions.LeftDown:
                {
                    indexAdditionX = -1;
                    indexAdditionZ = -1;
                    startPoint = 0;
                    step = (ChunkData.size + 1) * (ChunkData.size + 1);
                    otherChunkDot = step - 1;
                    offsetDown = ChunkData.size * (ChunkData.size + 1);
                    break;
                }
        }

        Debug.Log("=========================");
        Debug.Log("x = " + x);
        Debug.Log("z = " + z);
        Debug.Log("addX = " + indexAdditionX);
        Debug.Log("addZ = " + indexAdditionZ);
        Debug.Log("Direction = " + direction);
        Debug.Log("=========================");

        if ((chunks[x + indexAdditionX, z + indexAdditionZ] != null) && (chunks[x + indexAdditionX, z + indexAdditionZ].constructed == false))
            Debug.Log("FAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALSE");

        if ((chunks[x + indexAdditionX, z + indexAdditionZ] != null) && 
            (chunks[x + indexAdditionX, z + indexAdditionZ].constructed == true))
        {
            Debug.Log("SAD BUT TRUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUE");
            int dotsLength = (ChunkData.size + 1) * (ChunkData.size + 1);

            for (int i = startPoint; i < dotsLength + offsetDown; i += step)
            {
                chunks[x, z].dots[i].y = chunks[x + indexAdditionX, z + indexAdditionZ].dots[i + otherChunkDot].y;
                chunks[x, z].notCalculatedVecs[i] = 0;
            }
        }
    }
}
