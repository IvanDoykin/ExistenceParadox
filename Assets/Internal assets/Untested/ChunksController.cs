﻿using System.Collections;
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

public sealed class ChunksController : MonoBehaviour
{
    [SerializeField] GameObject chunk;

    private VerticesData verticesData;

    private const int trianglesPerChunk = ChunkData.size * ChunkData.size * 6;
    private const int chunkMetric = 16;
    private const int chunkBlockSize = 11; //not even number
    private const int halfChunkBlockSize = (chunkBlockSize - 1) / 2;

    private int zeroPointX;
    private int zeroPointZ;

    private int[] triangles;
    private int[] ordinalNumbers = new int[trianglesPerChunk];
    private Mesh mesh;

    private static ChunkData[,] chunks = new ChunkData[chunkBlockSize, chunkBlockSize];

    private static Vector3[] chunkBlockVertices = new Vector3[trianglesPerChunk * chunkBlockSize * chunkBlockSize];

    private static Vector3[] chunkBlockDots = new Vector3[(ChunkData.size + 1) * (ChunkData.size + 1) * chunkBlockSize * chunkBlockSize];
    private static bool[] dotIsCalculated = new bool[trianglesPerChunk * chunkBlockSize * chunkBlockSize];

    public static event ChunkGenerating.CallChunkLinking NeedLink;
    public static event Spaceman.SendChanging SendChange;

    private void Start()
    {
        verticesData = GetComponent<VerticesData>();

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        StarterCoordinating();
        StarterGenerating();

        CreateMesh();
        MeshCollider mesh_col = this.gameObject.AddComponent<MeshCollider>();
    }

    public void LinkChunk(CoordinatesData coordinatesData)
    {
        int x = coordinatesData.x - zeroPointX;
        int z = coordinatesData.z - zeroPointZ;
        LinkChunk(x, z);
    }

    public void ChunksUpdate(int offsetX, int offsetZ)
    {
        ChangeZeroPoints(offsetX, offsetZ);
        ChunksMassiveUpdate(offsetX, offsetZ);
        ChunksFilling();
    }

    private void ChangeZeroPoints(int offsetX, int offsetZ)
    {
        zeroPointX += offsetX;
        zeroPointZ += offsetZ;
    }

    private void ChunksMassiveUpdate(int offsetX, int offsetZ)
    {
        ChunkData[,] buffer = new ChunkData[chunkBlockSize, chunkBlockSize];

        for(int i = 0; i < chunkBlockSize; i++)
        {
            for(int j = 0; j < chunkBlockSize; j++)
            {
                bool iIsOverflow = ((i - offsetX) >= chunkBlockSize) || ((i - offsetX) < 0);
                bool jIsOverflow = ((j - offsetZ) >= chunkBlockSize) || ((j - offsetZ) < 0);

                if (iIsOverflow || jIsOverflow)
                {
                    if (chunks[i, j] != null)
                    {
                        DeleteChunkDots(i - halfChunkBlockSize, j - halfChunkBlockSize);
                        Destroy(chunks[i, j].gameObject);
                        chunks[i, j] = null;
                    }
                }

                else
                {
                    buffer[i - offsetX, j - offsetZ] = chunks[i, j];
                }
            }
        }

        for (int i = 0; i < chunkBlockSize; i++)
        {
            for (int j = 0; j < chunkBlockSize; j++)
            {
                chunks[i, j] = buffer[i, j];
            }
        }
    }

    private void ChunksFilling()
    {
        for(int i = 0; i < chunkBlockSize; i++)
        {
            for(int j = 0; j < chunkBlockSize; j++)
            {
                if(chunks[i, j] == null)
                {
                    CreateChunk(i - halfChunkBlockSize, j - halfChunkBlockSize);
                }
            }
        }
    }

    private void StarterCoordinating()
    {
        zeroPointX = (int)SetUpCoordinate(transform.position.x) + halfChunkBlockSize;
        zeroPointZ = (int)SetUpCoordinate(transform.position.z) + halfChunkBlockSize;
    }

    private void StarterGenerating()
    {
        CreateChunk(0,0); //chunks[0,0] isn't includes in algoryth of generating
        CreateBlockOfChunks();
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
        if (chunks[x + halfChunkBlockSize, z + halfChunkBlockSize] != null)
            return;

        GameObject createdChunk = Instantiate(chunk, new Vector3(PopUpCoordinate(x + zeroPointX), 0, PopUpCoordinate(z + zeroPointZ)), new Quaternion(0, 0, 0, 0), gameObject.transform);
        chunks[x + halfChunkBlockSize, z + halfChunkBlockSize] = createdChunk.GetComponent<ChunkData>();
        AddChunkDots(x + halfChunkBlockSize, z + halfChunkBlockSize);
    }

    private void AddChunkDots(int x, int z)
    {
        for (int i = 0; i < (ChunkData.size + 1) * (ChunkData.size + 1); i++)
        {
            chunkBlockDots[i + x * (ChunkData.size + 1) + z * chunkBlockSize * (ChunkData.size + 1)] = chunks[x, z].dots[i];
            dotIsCalculated[i + x * (ChunkData.size + 1) + z * chunkBlockSize * (ChunkData.size + 1)] = true;
        }
    }

    private void DeleteChunkDots(int x, int z)
    {
        for (int i = 0; i < (ChunkData.size + 1) * (ChunkData.size + 1); i++)
        {
            chunkBlockDots[i + x * (ChunkData.size + 1) + z * chunkBlockSize * (ChunkData.size + 1)] = Vector3.zero;
            dotIsCalculated[i + x * (ChunkData.size + 1) + z * chunkBlockSize * (ChunkData.size + 1)] = false;
        }
    }

    private void LinkChunk(int x, int z)
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


        if ((chunks[x + indexAdditionX + halfChunkBlockSize, z + indexAdditionZ + halfChunkBlockSize] != null) &&
          (chunks[x + indexAdditionX + halfChunkBlockSize, z + indexAdditionZ + halfChunkBlockSize].constructed == true))
        {
            int dotsLength = (ChunkData.size + 1) * (ChunkData.size + 1);

            for (int i = startPoint; i < dotsLength + offsetDown; i += step)
            {
                chunks[x + halfChunkBlockSize, z + halfChunkBlockSize].dots[i].y = chunks[x + indexAdditionX + halfChunkBlockSize, z + indexAdditionZ + halfChunkBlockSize].dots[i + otherChunkDot].y;
                chunks[x + halfChunkBlockSize, z + halfChunkBlockSize].notCalculatedVecs[i] = 0;
            }
        }

    }

    private void CreateMesh()
    {
        CreateTriangles();
        CreateShape();
        UpdateMesh();
    }
    
    private void CreateTriangles()
    {
        Debug.Log("SIZE_vert = " + chunkBlockVertices.Length);
        Debug.Log("SIZE_dots = " + chunkBlockDots.Length);
        //create triangles fromchunk.ordinalNumbers
        #region upper_tr 
        //generating triangles from dots (x, x+5, x+1) last dot = (dots.len - 1) - (size + 1)
        int index = 0;
        for (int x = 0; x < chunkBlockDots.Length - 2 - ChunkData.size; x++)
        {
            Debug.Log("Create triangle: " + x + "; " + (x + (ChunkData.size + 1)) + "; " + (x + 1));
            Debug.Log("VertexIndex = " + index);

            chunkBlockVertices[index] = chunkBlockDots[x];
            chunkBlockVertices[index + 1] = chunkBlockDots[x + (ChunkData.size + 1)];
            chunkBlockVertices[index + 2] = chunkBlockDots[x + 1];
            index += 3;
        }
        #endregion
        #region lower_tr
        //generating triangles fromchunk.ordinalNumbers (x, x+4, x+5) last dot = chunk.ordinalNumbers.len - (ChunkData.size + 1)
        for (int x = 0; index < chunkBlockVertices.Length; x++)
        {
            Debug.Log("Create triangle(2): " + x + "; " + (x + (ChunkData.size + 1)) + "; " + (x + 1));
            Debug.Log("VertexIndex = " + index);

            chunkBlockVertices[index] = chunkBlockDots[x];
            chunkBlockVertices[index + 1] = chunkBlockDots[x + ChunkData.size];
            chunkBlockVertices[index + 2] = chunkBlockDots[x + (ChunkData.size + 1)];
            index += 3;
        }
        #endregion
        #region initializing_ordinal_numbers_massive
        //massive for creating triangles massive.len = "triangles amounts" * 3
        for (int h = 0; h < ordinalNumbers.Length; h++)
        {
            ordinalNumbers[h] = h;
        }
        #endregion
    }
    
    private void CreateShape()
    {
        //setting up dots and massive of ordinal numbers
        verticesData.generatedVertices = chunkBlockVertices;
        triangles = ordinalNumbers;
    }

    private void UpdateMesh()
    {
        //setting up verticles and triangles with some fixing
        mesh.Clear();

        mesh.vertices = verticesData.generatedVertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
