using UnityEngine;

public class Chunk : MonoBehaviour
{
    //in code u can see '16' - it is not "magic" number, even not const. It length (and width) of chunk. U can't change this

    protected Chunk chunk;

    protected float range = 10; //height 'strength'
    protected float smaller_range = 3;//allow change generating mode (smaller - flatter, bigger - 'hilly')

    protected const int size = 8;
    protected const float size_normaller = 16 / size; // chunk has size 16*16. We need keep it with different sizes

    public int[] Not_calculated_vecs = new int[(size + 1) * (size + 1)]; //using for determinate not calculated verticles 
    public Vector3[] Vecs = new Vector3[(size + 1) * (size + 1)]; //main dots = (size + 1) ^ 2 //for saving

    protected Vector3[] vertics = new Vector3[size * size * 6]; //vertics = dots (IMPORTANT!)
    protected int[] dots = new int[size * size * 6]; //dots = verics (IMPORTANT!)

    public bool Constructed = false; //is chunck constructed?
    public int Coord_x = 0; 
    public int Coord_z = 0; 
    public string Chunk_name = "0;0"; //for saving
    public Person Player;

    protected Mesh mesh;
    protected Vector3[] vertices;
    protected int[] triangles;

}
