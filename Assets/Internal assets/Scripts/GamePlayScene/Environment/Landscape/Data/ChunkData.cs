using UnityEngine;

public class ChunkData : MonoBehaviour
{
    public float range = 2; //height 'strength'
    public float rangeOffset = 0;
    public float smallerRange = 0; //allow change generating mode (smaller - flatter, bigger - 'hilly')

    [HideInInspector] public const int metric = 16;
    [HideInInspector] public const int size = 8;
    [HideInInspector] public const float sizeNormaller = metric / size; // chunk has size 16*16. We need keep it with different sizes

    [HideInInspector] public int[] notCalculatedVecs = new int[(size + 1) * (size + 1)]; //using for determinate not calculated verticles 
    public Vector3[] dots = new Vector3[(size + 1) * (size + 1)]; //main dots = (size + 1) ^ 2 //for saving

    [HideInInspector] public Vector3[] vertices = new Vector3[size * size * 6]; //vertics = ordinalNumbers (IMPORTANT!)
    [HideInInspector] public int[] ordinalNumbers = new int[size * size * 6]; //ordinalNumbers = vertics (IMPORTANT!)

    [HideInInspector] public bool constructed = false; //is chunck constructed?

    [HideInInspector] public Mesh mesh;
    [HideInInspector] public int[] triangles;

    [HideInInspector] public int argX = 0;
    [HideInInspector] public int argZ = 0;
}
