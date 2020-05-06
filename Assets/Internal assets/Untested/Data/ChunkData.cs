using UnityEngine;

public class ChunkData : MonoBehaviour
{
    [HideInInspector] public float range = 10; //height 'strength'
    [HideInInspector] public float smallerRange = 3;//allow change generating mode (smaller - flatter, bigger - 'hilly')

<<<<<<< HEAD
    [HideInInspector] public const int size = 8;
    [HideInInspector] public const float sizeNormaller = 16 / size; // chunk has size 16*16. We need keep it with different sizes

    [HideInInspector] public int[] notCalculatedVecs = new int[(size + 1) * (size + 1)]; //using for determinate not calculated verticles 
    [HideInInspector] public Vector3[] dots = new Vector3[(size + 1) * (size + 1)]; //main dots = (size + 1) ^ 2 //for saving
=======
    [HideInInspector] public const int metric = 16;
    [HideInInspector] public const int size = 8;
    [HideInInspector] public const float sizeNormaller = metric / size; // chunk has size 16*16. We need keep it with different sizes

    [HideInInspector] public int[] notCalculatedVecs = new int[(size + 1) * (size + 1)]; //using for determinate not calculated verticles 
     public Vector3[] dots = new Vector3[(size + 1) * (size + 1)]; //main dots = (size + 1) ^ 2 //for saving
>>>>>>> Chunk_Gen

    [HideInInspector] public Vector3[] vertices = new Vector3[size * size * 6]; //vertics = ordinalNumbers (IMPORTANT!)
    [HideInInspector] public int[] ordinalNumbers = new int[size * size * 6]; //ordinalNumbers = verics (IMPORTANT!)

    [HideInInspector] public bool constructed = false; //is chunck constructed?

    [HideInInspector] public Mesh mesh;
    [HideInInspector] public int[] triangles;
}
