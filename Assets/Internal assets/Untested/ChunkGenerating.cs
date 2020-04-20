using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ChunkData))]
[RequireComponent(typeof(VerticesData))]
[RequireComponent(typeof(CoordinatesData))]

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class ChunkGenerating : MonoBehaviour
{
    private ChunkData chunk;
    private VerticesData verticesData;
    private CoordinatesData coordinatesData;

    private ChunkNameSetuper chunkNameSetuper;
    private ChunksController chunksController;

    public delegate void GeneratingEvents();
    public static event GeneratingEvents GeneratingDone;

    public delegate void CallChunkLinking(CoordinatesData coordinatesData);
    public static event CallChunkLinking NeedLink;

    private void Start()
    { 
        chunksController = GetComponentInParent<ChunksController>();

        chunk = GetComponent<ChunkData>();

        verticesData = GetComponent<VerticesData>();
        coordinatesData = GetComponent<CoordinatesData>();
        chunkNameSetuper = GetComponent<ChunkNameSetuper>();

        GeneratingDone += chunkNameSetuper.SetName;
        NeedLink += chunksController.LinkChunk;

        InitializeChunk();

        bool created = false; //temp simplify

        GeneratingDone();
        GeneratingDone -= chunkNameSetuper.SetName;

        if (!created)
        {

            NeedLink(coordinatesData);
            NeedLink -= chunksController.LinkChunk;

            //link this chunk
            chunk.constructed = true;
            DiamondSquare(ChunkData.size);
        }

        CreateMesh();
        MeshCollider mesh_col = this.gameObject.AddComponent<MeshCollider>();


    }

    private void CreateMesh()
    {
        CreateTriangles();
        CreateShape();
        UpdateMesh();
    }

    private void InitializeChunk()
    {
        for (int i = 0; i < chunk.notCalculatedVecs.Length; i++)
        {
            chunk.notCalculatedVecs[i] = 1;
        }

        chunk.mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = chunk.mesh;
        InitializeVectors();
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
    }

    private void DiamondSquare(int mesh_size, int coord_offset = 0)
    {
        //if simple - take 2 dots height and interpolate height using dots between them with random offset

        if (mesh_size == 1) return;

        int _size = mesh_size + 1;
        //for more smooth generating
        float multiply = mesh_size / ChunkData.size;

        //corner dots index
        int left_down_corner_index = 0 + coord_offset;
        int right_down_corner_index = mesh_size + coord_offset;
        int left_up_corner_index = (ChunkData.size + 1) * mesh_size + coord_offset;
        int right_up_corner_index = (ChunkData.size + 2) * mesh_size + coord_offset;
        int middle_index = (left_down_corner_index + right_up_corner_index) / 2;

        //directional dots index
        int up_index = (left_up_corner_index + right_up_corner_index) / 2;
        int left_index = (left_down_corner_index + left_up_corner_index) / 2;
        int right_index = (right_down_corner_index + right_up_corner_index) / 2;
        int down_index = (left_down_corner_index + right_down_corner_index) / 2;

        //if the first function calling
        if (mesh_size == ChunkData.size)
        {
            //then initialize corner dots
            chunk.dots[0].y += (Random.Range(-chunk.range, chunk.range) + Random.Range(-chunk.smallerRange, chunk.smallerRange) * multiply) * chunk.notCalculatedVecs[0];
            chunk.notCalculatedVecs[0] = 0;

            chunk.dots[right_down_corner_index].y += (Random.Range(-chunk.range, chunk.range) + Random.Range(-chunk.smallerRange, chunk.smallerRange) * multiply) * chunk.notCalculatedVecs[right_down_corner_index];
            chunk.notCalculatedVecs[right_down_corner_index] = 0;

            chunk.dots[left_up_corner_index].y += (Random.Range(-chunk.range, chunk.range) + +Random.Range(-chunk.smallerRange, chunk.smallerRange) * multiply) * chunk.notCalculatedVecs[left_up_corner_index];
            chunk.notCalculatedVecs[left_up_corner_index] = 0;

            chunk.dots[right_up_corner_index].y += (Random.Range(-chunk.range, chunk.range) + Random.Range(-chunk.smallerRange, chunk.smallerRange) * multiply) * chunk.notCalculatedVecs[right_up_corner_index];
            chunk.notCalculatedVecs[right_up_corner_index] = 0;
        }

        //initialize middle and directional dots

        chunk.dots[middle_index].y += ((chunk.dots[left_down_corner_index].y +
            chunk.dots[right_down_corner_index].y +
            chunk.dots[left_up_corner_index].y +
            chunk.dots[right_up_corner_index].y) / 4 + Random.Range(-chunk.smallerRange, chunk.smallerRange) * multiply) * chunk.notCalculatedVecs[middle_index];
        chunk.notCalculatedVecs[middle_index] = 0;

        chunk.dots[up_index].y += ((chunk.dots[left_up_corner_index].y + chunk.dots[middle_index].y + chunk.dots[right_up_corner_index].y) / 3 + Random.Range(-chunk.smallerRange, chunk.smallerRange) * multiply) * chunk.notCalculatedVecs[up_index];
        chunk.notCalculatedVecs[up_index] = 0;

        chunk.dots[left_index].y += ((chunk.dots[left_up_corner_index].y + chunk.dots[middle_index].y + chunk.dots[left_down_corner_index].y) / 3 + Random.Range(-chunk.smallerRange, chunk.smallerRange) * multiply) * chunk.notCalculatedVecs[left_index];
        chunk.notCalculatedVecs[left_index] = 0;

        chunk.dots[right_index].y += ((chunk.dots[right_up_corner_index].y + chunk.dots[middle_index].y + chunk.dots[right_down_corner_index].y) / 3 + Random.Range(-chunk.smallerRange, chunk.smallerRange) * multiply) * chunk.notCalculatedVecs[right_index];
        chunk.notCalculatedVecs[right_index] = 0;

        chunk.dots[down_index].y += ((chunk.dots[left_down_corner_index].y + chunk.dots[middle_index].y + chunk.dots[right_down_corner_index].y) / 3 + Random.Range(-chunk.smallerRange, chunk.smallerRange) * multiply) * chunk.notCalculatedVecs[down_index];
        chunk.notCalculatedVecs[down_index] = 0;


        DiamondSquare(mesh_size / 2, coord_offset);
        DiamondSquare(mesh_size / 2, (left_down_corner_index + right_down_corner_index) / 2);
        DiamondSquare(mesh_size / 2, (left_down_corner_index + left_up_corner_index) / 2);
        DiamondSquare(mesh_size / 2, (left_down_corner_index + right_up_corner_index) / 2);

    }

    private void InitializeVectors()
    {
        for (int i = 0; i <= ChunkData.size; i++)
        {
            for (int j = 0; j <= ChunkData.size; j++)
            {
                chunk.dots[(i * (ChunkData.size + 1)) + j] = new Vector3(j * ChunkData.sizeNormaller, 0, i * ChunkData.sizeNormaller); //coord for ever dot
            }
        }
    }
}
