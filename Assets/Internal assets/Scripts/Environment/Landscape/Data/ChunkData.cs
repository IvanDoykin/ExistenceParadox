using UnityEngine;

public class ChunkData : Saveable
{
    public float range = 2; //height 'strength'
    public float rangeOffset;
    public float smallerRange = 0; //allow change generating mode (smaller - flatter, bigger - 'hilly')

    [HideInInspector] public const int Metric = 16;
    [HideInInspector] public const int Size = 8;
    [HideInInspector] public const float SizeNormaller = Metric / Size; // chunk has size 16*16. We need keep it with different sizes

    [HideInInspector] public int[] notCalculatedVecs = new int[(Size + 1) * (Size + 1)]; //using for determinate not calculated verticles 
    public Vector3[] dots = new Vector3[(Size + 1) * (Size + 1)]; //main dots = (size + 1) ^ 2 //for saving
    public Vector3[] generatedVertices;
    [HideInInspector] public Vector3[] vertices = new Vector3[Size * Size * 6]; //vertics = ordinalNumbers (IMPORTANT!)
    [HideInInspector] public int[] ordinalNumbers = new int[Size * Size * 6]; //ordinalNumbers = vertics (IMPORTANT!)

    [HideInInspector] public bool readyForUpdate = false;
    [HideInInspector] public bool constructed = false; //is chunck constructed?
    [HideInInspector] public bool fullUpdated = false;

    [HideInInspector] public int argX = 0;
    [HideInInspector] public int argZ = 0;

    public Mesh mesh;
    [HideInInspector] public int[] triangles;

    public void SetStartData(bool FullUpdated, Vector3[] Dots)
    {
        fullUpdated = FullUpdated;
        dots = Dots;
    }

    private void OnDestroy()
    {
        Debug.Log("Chunk destroing");
    }
}
