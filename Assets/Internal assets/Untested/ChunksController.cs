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

[RequireComponent(typeof(ChunksControllerData))]
[RequireComponent(typeof(ChunksBlockAssembler))]
public class ChunksController : MonoBehaviour
{
    ChunksControllerData controllerData;
    ChunksBlockAssembler chunksAssembler;

    public static event ChunkGenerating.CallChunkLinking NeedLink;
    public static event Spaceman.SendChanging SendChange;

    public delegate void ChunkAssembly();
    public static event ChunkAssembly AssemblyStart;

    private void Start()
    {
        controllerData = GetComponent<ChunksControllerData>();
        chunksAssembler = GetComponent<ChunksBlockAssembler>();

        StarterCoordinating();
        StarterGenerating();

        AssemblyStart += chunksAssembler.ReassemblyChunksBlock; 
    }

    public void LinkChunk(CoordinatesData coordinatesData)
    {
        int x = coordinatesData.x - controllerData.zeroPointX;
        int z = coordinatesData.z - controllerData.zeroPointZ;
        LinkOneChunk(x, z);
    }

    public void ChunksUpdate(int offsetX, int offsetZ)
    {
        chunksAssembler.SetNeedGeneratedChunks(Mathf.Abs(offsetX) * ChunksControllerData.chunksBlockSize + Mathf.Abs(offsetZ) * ChunksControllerData.chunksBlockSize - Mathf.Abs(offsetX) * Mathf.Abs(offsetZ));

        Debug.Log("=====Chunks Update=====");
        Debug.Log("offsetX = " + offsetX);
        Debug.Log("offsetZ = " + offsetZ);

        ChangeZeroPoints(offsetX, offsetZ);
        ChunksMassiveUpdate(offsetX, offsetZ);
        ChunksFilling();
    }

    private void ChangeZeroPoints(int offsetX, int offsetZ)
    {
        Debug.Log("=====Change Zero Points=====");
        Debug.Log("Was: controllerData.zeroPointX = " + controllerData.zeroPointX + " and controllerData.zeroPointZ = " + controllerData.zeroPointZ);

        controllerData.zeroPointX += offsetX;
        controllerData.zeroPointZ += offsetZ;

        Debug.Log("Is: controllerData.zeroPointX = " + controllerData.zeroPointX + " and controllerData.zeroPointZ = " + controllerData.zeroPointZ);
    }

    private void ChunksMassiveUpdate(int offsetX, int offsetZ)
    {
        ChunkData[,] buffer = new ChunkData[ChunksControllerData.chunksBlockSize, ChunksControllerData.chunksBlockSize];

        for(int i = 0; i < ChunksControllerData.chunksBlockSize; i++)
        {
            for(int j = 0; j < ChunksControllerData.chunksBlockSize; j++)
            {
                bool iIsOverflow = ((i - offsetX) >= ChunksControllerData.chunksBlockSize) || ((i - offsetX) < 0);
                bool jIsOverflow = ((j - offsetZ) >= ChunksControllerData.chunksBlockSize) || ((j - offsetZ) < 0);

                if (iIsOverflow || jIsOverflow)
                {
                    if (ChunksControllerData.chunks[i, j] != null)
                    {
                        Destroy(ChunksControllerData.chunks[i, j].gameObject);
                        ChunksControllerData.chunks[i, j] = null;
                    }

                    else
                    {
                        Debug.Log("i = " + i + "; j = " + j + " NULL");
                    }
                }

                else
                {
                    buffer[i - offsetX, j - offsetZ] = ChunksControllerData.chunks[i, j];
                }
            }
        }

        for (int i = 0; i < ChunksControllerData.chunksBlockSize; i++)
        {
            for (int j = 0; j < ChunksControllerData.chunksBlockSize; j++)
            {
                ChunksControllerData.chunks[i, j] = buffer[i, j];
            }
        }
    }

    private void ChunksFilling()
    {
        for(int i = 0; i < ChunksControllerData.chunksBlockSize; i++)
        {
            for(int j = 0; j < ChunksControllerData.chunksBlockSize; j++)
            {
                if(ChunksControllerData.chunks[i, j] == null)
                {
                    CreateChunk(i - ChunksControllerData.halfChunkBlockSize, j - ChunksControllerData.halfChunkBlockSize);
                }
            }
        }
    }

    private void StarterCoordinating()
    {
        controllerData.zeroPointX = (int)SetUpCoordinate(transform.position.x) + ChunksControllerData.halfChunkBlockSize;
        controllerData.zeroPointZ = (int)SetUpCoordinate(transform.position.z) + ChunksControllerData.halfChunkBlockSize;
    }

    private void StarterGenerating()
    {
        CreateChunk(0,0); //chunks[0,0] isn't includes in algoryth of generating
        CreateBlockOfChunks();
    }

    private void CreateBlockOfChunks()
    {
        for (int round = 1; round < ChunksControllerData.halfChunkBlockSize + 1; round++)
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
        if (ChunksControllerData.chunks[x + ChunksControllerData.halfChunkBlockSize, z + ChunksControllerData.halfChunkBlockSize] != null)
            return;

        Debug.Log("CreateChunk: x = " + x + " z = " + z);
        GameObject createdChunk = Instantiate(controllerData.chunk, new Vector3(PopUpCoordinate(x + controllerData.zeroPointX), 0, PopUpCoordinate(z + controllerData.zeroPointZ)), new Quaternion(0, 0, 0, 0), gameObject.transform);
        ChunksControllerData.chunks[x + ChunksControllerData.halfChunkBlockSize, z + ChunksControllerData.halfChunkBlockSize] = createdChunk.GetComponent<ChunkData>();
    }

    private void LinkChunks(int x, int z)
    {
        for (int round = 1; round < ChunksControllerData.halfChunkBlockSize + 1; round++)
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

    private void LinkOneChunk(int x, int z)
    {
        bool right = x != ChunksControllerData.halfChunkBlockSize;
        bool left = x != -ChunksControllerData.halfChunkBlockSize;
        bool up = z != ChunksControllerData.halfChunkBlockSize;
        bool down = z != -ChunksControllerData.halfChunkBlockSize;

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


        if ((ChunksControllerData.chunks[x + indexAdditionX + ChunksControllerData.halfChunkBlockSize, z + indexAdditionZ + ChunksControllerData.halfChunkBlockSize] != null) &&
          (ChunksControllerData.chunks[x + indexAdditionX + ChunksControllerData.halfChunkBlockSize, z + indexAdditionZ + ChunksControllerData.halfChunkBlockSize].constructed == true))
        {
            int dotsLength = (ChunkData.size + 1) * (ChunkData.size + 1);

            for (int i = startPoint; i < dotsLength + offsetDown; i += step)
            {
                ChunksControllerData.chunks[x + ChunksControllerData.halfChunkBlockSize, z + ChunksControllerData.halfChunkBlockSize].dots[i].y = ChunksControllerData.chunks[x + indexAdditionX + ChunksControllerData.halfChunkBlockSize, z + indexAdditionZ + ChunksControllerData.halfChunkBlockSize].dots[i + otherChunkDot].y;
                ChunksControllerData.chunks[x + ChunksControllerData.halfChunkBlockSize, z + ChunksControllerData.halfChunkBlockSize].notCalculatedVecs[i] = 0;
            }
        }

    }
}
