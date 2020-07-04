using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChunkData))]
[RequireComponent(typeof(VerticesData))]
[RequireComponent(typeof(CoordinatesData))]

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class SecondaryChunkGenerating : MonoBehaviour, IEventTrigger
{
    static bool first = true;

    public delegate void Smooth(int x, int z);
    public static event Smooth SmoothMe;

    private ChunkData chunk;
    private VerticesData verticesData;

    private ChunkNameSetuper chunkNameSetuper;
    private EdgeSmoother edgeSmoother;

    public delegate void GeneratingEvents();
    public static event GeneratingEvents GeneratingDone;

    public static event PrimaryChunkGenerating.CallPrimaryGenerating PrimaryGeneratingDone;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (chunk.fullUpdated)
                return;

            gameObject.GetComponent<MeshFilter>().mesh.Clear();

            CreateMesh();
            Destroy(GetComponent<MeshCollider>());
            MeshCollider mesh_col = this.gameObject.AddComponent<MeshCollider>();
        }
    }

    private void Start()
    {
        verticesData = GetComponent<VerticesData>();
        chunk = GetComponent<ChunkData>();
        chunkNameSetuper = GetComponent<ChunkNameSetuper>();

        edgeSmoother = GetComponentInParent<EdgeSmoother>();

        SmoothMe += edgeSmoother.Smooth;
    }

    public void SecondaryGenerating()
    {
        //GeneratingDone += chunksAssembler.ChunkIsReady;
        GeneratingDone += chunkNameSetuper.SetName;

        for (int i = 0; i < chunk.dots.Length; i++)
        {
            if (((i % (ChunkData.size + 1)) != 0) && ((i + 1) % (ChunkData.size + 1) != 0) && (i < (ChunkData.size * (ChunkData.size + 1) + 1)) && (i > ChunkData.size))
            {
                if (first)
                {
                    Debug.Log("dot..... " + i);
                }
                chunk.dots[i].y = Average(chunk.dots[i - 1].y, chunk.dots[i + (ChunkData.size + 1)].y, chunk.dots[i - (ChunkData.size + 1)].y, chunk.dots[i + 1].y);
            }
            else
            {

            }
        }

        first = false;
        CreateMesh();
        MeshCollider mesh_col = this.gameObject.AddComponent<MeshCollider>();

        GeneratingDone();
        GeneratingDone -= chunkNameSetuper.SetName;
        //GeneratingDone -= chunksAssembler.ChunkIsReady;

        SmoothMe(chunk.argX, chunk.argZ);
        SmoothMe -= edgeSmoother.Smooth;
    }

    public static float Average(params float[] numbers)
    {
        float sum = 0;
        foreach (float f in numbers)
        {
            sum += f;
        }

        return (sum / numbers.Length);
    }

    public void CreateMesh()
    {
        CreateTriangles();
        CreateShape();
        UpdateMesh();
    }

    private void CreateTriangles()
    {
        //create triangles fromchunk.ordinalNumbers
        #region upper_tr 
        //generating triangles from dots (x, x+5, x+1) last dot = (dots.len - 1) - (size + 1)
        int index = 0;
        for (int x = 0; x < chunk.dots.Length - 2 - ChunkData.size; x++)
        {
            if (((x + 1) % (ChunkData.size + 1) == 0) && (x != 0))
                continue;

            chunk.vertices[index] = chunk.dots[x];
            chunk.vertices[index + 1] = chunk.dots[x + (ChunkData.size + 1)];
            chunk.vertices[index + 2] = chunk.dots[x + 1];
            index += 3;
        }
        #endregion
        #region lower_tr
        //generating triangles fromchunk.ordinalNumbers (x, x+4, x+5) last dot =chunk.ordinalNumbers.len - (ChunkData.size + 1)
        for (int x = 0; x < chunk.dots.Length - 1 - ChunkData.size; x++)
        {
            if (x % (ChunkData.size + 1) == 0)
                continue;

            chunk.vertices[index] = chunk.dots[x];
            chunk.vertices[index + 1] = chunk.dots[x + ChunkData.size];
            chunk.vertices[index + 2] = chunk.dots[x + (ChunkData.size + 1)];
            index += 3;
        }
        #endregion
        #region initializing_ordinal_numbers_massive
        //massive for creating triangles massive.len = "triangles amounts" * 3
        for (int h = 0; h < chunk.ordinalNumbers.Length; h++)
        {
            chunk.ordinalNumbers[h] = h;
        }
        #endregion
    }

    private void CreateShape()
    {
        //setting up dots and massive of ordinal numbers
        verticesData.generatedVertices = chunk.vertices;
        chunk.triangles = chunk.ordinalNumbers;
    }

    private void UpdateMesh()
    {
        //setting up verticles and triangles with some fixing
        chunk.mesh.Clear();

        chunk.mesh.vertices = verticesData.generatedVertices;
        chunk.mesh.triangles = chunk.triangles;

        chunk.mesh.RecalculateNormals();
        chunk.mesh.RecalculateBounds();

        gameObject.GetComponent<MeshFilter>().mesh = chunk.mesh;
    }

    public void TriggerEvent(string eventName, params object[] arguments)
    {
        ManagerEvents.CheckTriggeringEvent(eventName);
    }
}
