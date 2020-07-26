using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChunkData))]
[RequireComponent(typeof(VerticesData))]
[RequireComponent(typeof(CoordinatesData))]

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class SecondaryChunkGenerating : MonoBehaviour, IEventSub
{
    [SerializeField] private EventsCollection ChunkCreated;

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
        if (Input.GetKeyDown(KeyCode.C))
        {
            SaveChunk saveChunk = new SaveChunk();

            saveChunk.WriteData(chunk, chunk.gameObject.GetComponent<ChunkNameData>().value);
            Debug.Log("all right");
        }

        if (chunk.readyForUpdate)
        {
            chunk.readyForUpdate = false;
            if (chunk.fullUpdated)
                return;

            CreateMesh();
            GetComponent<MeshCollider>().sharedMesh = chunk.mesh;
        }
    }

    private void Start()
    {
        Subscribe();

        verticesData = GetComponent<VerticesData>();
        chunk = GetComponent<ChunkData>();
        chunkNameSetuper = GetComponent<ChunkNameSetuper>();

        edgeSmoother = GetComponentInParent<EdgeSmoother>();

        SmoothMe += edgeSmoother.Smooth;
    }

    public void SecondaryGenerating()
    {
        if (gameObject.GetComponent<PrimaryChunkGenerating>().created)
        {
            GeneratingDone();
            GeneratingDone -= chunkNameSetuper.SetName;

            CreateMesh();
            MeshCollider meshCol = this.gameObject.AddComponent<MeshCollider>();

        }

        GeneratingDone += chunkNameSetuper.SetName;

        for (int i = 0; i < chunk.dots.Length; i++)
        {
            if (((i % (ChunkData.Size + 1)) != 0) && ((i + 1) % (ChunkData.Size + 1) != 0) && (i < (ChunkData.Size * (ChunkData.Size + 1) + 1)) && (i > ChunkData.Size))
            {
                chunk.dots[i].y = Average(chunk.dots[i - 1].y, chunk.dots[i + (ChunkData.Size + 1)].y, chunk.dots[i - (ChunkData.Size + 1)].y, chunk.dots[i + 1].y);
            }
        }

        GeneratingDone();
        GeneratingDone -= chunkNameSetuper.SetName;

        CreateMesh();
        MeshCollider mesh_col = this.gameObject.AddComponent<MeshCollider>();

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
        for (int x = 0; x < chunk.dots.Length - 2 - ChunkData.Size; x++)
        {
            if (((x + 1) % (ChunkData.Size + 1) == 0) && (x != 0))
                continue;

            chunk.vertices[index] = chunk.dots[x];
            chunk.vertices[index + 1] = chunk.dots[x + (ChunkData.Size + 1)];
            chunk.vertices[index + 2] = chunk.dots[x + 1];
            index += 3;
        }
        #endregion
        #region lower_tr
        //generating triangles fromchunk.ordinalNumbers (x, x+4, x+5) last dot =chunk.ordinalNumbers.len - (ChunkData.size + 1)
        for (int x = 0; x < chunk.dots.Length - 1 - ChunkData.Size; x++)
        {
            if (x % (ChunkData.Size + 1) == 0)
                continue;

            chunk.vertices[index] = chunk.dots[x];
            chunk.vertices[index + 1] = chunk.dots[x + ChunkData.Size];
            chunk.vertices[index + 2] = chunk.dots[x + (ChunkData.Size + 1)];
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

    private void ChangeUpdateState()
    {
        chunk.readyForUpdate = true;
    }

    public void Subscribe()
    {
        ManagerEvents.StartListening(ChunkCreated.currentEvent, ChangeUpdateState);
    }

    public void UnSubscribe()
    {
        ManagerEvents.StopListening(ChunkCreated.currentEvent, ChangeUpdateState);
    }
}
