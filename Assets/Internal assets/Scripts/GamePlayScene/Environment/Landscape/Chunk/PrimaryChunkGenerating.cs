using UnityEngine;

[RequireComponent(typeof(ChunkData))]
[RequireComponent(typeof(VerticesData))]
[RequireComponent(typeof(CoordinatesData))]

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(SecondaryChunkGenerating))]

public class PrimaryChunkGenerating : MonoBehaviour
{
    public bool created;

    private LoadChunk loadChunk;
    private SecondaryChunkGenerating secondaryChunkGenerating;
    private ChunkData chunk;
    private CoordinatesData coordinatesData;
    private ChunksBlock chunksController;

    public delegate void CallChunkLinking(CoordinatesData coordinatesData);
    public static event CallChunkLinking NeedLink;

    public delegate void CallPrimaryGenerating();
    public static event CallPrimaryGenerating PrimaryGeneratingDone;

    private void Start()
    {
        loadChunk = GetComponent<LoadChunk>();

        chunksController = GetComponentInParent<ChunksBlock>();

        secondaryChunkGenerating = GetComponent<SecondaryChunkGenerating>();
        chunk = GetComponent<ChunkData>();
        coordinatesData = GetComponent<CoordinatesData>();

        NeedLink += chunksController.LinkChunk;
        PrimaryGeneratingDone += secondaryChunkGenerating.SecondaryGenerating;

        InitializeChunk();
        chunk.position = gameObject.transform.position;

        created = loadChunk.TryLoadData(ref chunk, gameObject.GetComponent<ChunkNameData>().value);

        if (!created)
        {
            Debug.Log("Not load");
            NeedLink(coordinatesData);
            NeedLink -= chunksController.LinkChunk;

            DiamondSquare(ChunkData.Size);
            PrimaryGeneratingDone();
        }
        
        else
            secondaryChunkGenerating.CreateMesh();

        chunk.constructed = true;

        NeedLink -= chunksController.LinkChunk;
        PrimaryGeneratingDone -= secondaryChunkGenerating.SecondaryGenerating;
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

    private void DiamondSquare(int mesh_size, int coord_offset = 0)
    {
        //if simple - take 2 dots height and interpolate height using dots between them with random offset

        if (mesh_size == 1) return;

        //for more smooth generating
        float multiply = mesh_size / ChunkData.Size;

        //corner dots index
        int left_down_corner_index = 0 + coord_offset;
        int right_down_corner_index = mesh_size + coord_offset;
        int left_up_corner_index = (ChunkData.Size + 1) * mesh_size + coord_offset;
        int right_up_corner_index = (ChunkData.Size + 2) * mesh_size + coord_offset;
        int middle_index = (left_down_corner_index + right_up_corner_index) / 2;

        //directional dots index
        int up_index = (left_up_corner_index + right_up_corner_index) / 2;
        int left_index = (left_down_corner_index + left_up_corner_index) / 2;
        int right_index = (right_down_corner_index + right_up_corner_index) / 2;
        int down_index = (left_down_corner_index + right_down_corner_index) / 2;

        //if the first function calling
        if (mesh_size == ChunkData.Size)
        {
            //then initialize corner dots
            chunk.dots[0].y += (Random.Range(-chunk.range + chunk.rangeOffset, chunk.range + chunk.rangeOffset) + Random.Range(-chunk.smallerRange, chunk.smallerRange) * multiply) * chunk.notCalculatedVecs[0];
            chunk.notCalculatedVecs[0] = 0;

            chunk.dots[right_down_corner_index].y += (Random.Range(-chunk.range + chunk.rangeOffset, chunk.range + chunk.rangeOffset) + Random.Range(-chunk.smallerRange, chunk.smallerRange) * multiply) * chunk.notCalculatedVecs[right_down_corner_index];
            chunk.notCalculatedVecs[right_down_corner_index] = 0;

            chunk.dots[left_up_corner_index].y += (Random.Range(-chunk.range + chunk.rangeOffset, chunk.range + chunk.rangeOffset) + Random.Range(-chunk.smallerRange, chunk.smallerRange) * multiply) * chunk.notCalculatedVecs[left_up_corner_index];
            chunk.notCalculatedVecs[left_up_corner_index] = 0;

            chunk.dots[right_up_corner_index].y += (Random.Range(-chunk.range + chunk.rangeOffset, chunk.range + chunk.rangeOffset) + Random.Range(-chunk.smallerRange, chunk.smallerRange) * multiply) * chunk.notCalculatedVecs[right_up_corner_index];
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
        for (int i = 0; i <= ChunkData.Size; i++)
        {
            for (int j = 0; j <= ChunkData.Size; j++)
            {
                chunk.dots[(i * (ChunkData.Size + 1)) + j] = new Vector3(j * ChunkData.SizeNormaller, 0, i * ChunkData.SizeNormaller); //coord for ever dot
            }
        }
    }
}
