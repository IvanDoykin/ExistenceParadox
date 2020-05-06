using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Created_Chunk : Chunk
{

    private void Start()
    {
        Initialize_Chunk();
        // bool created = Chunks_Controller.Reading_From_File(ref chunk, Player);
        bool created = false;
        if (!created)
        {
            Chunks_Controller.Set_Chunk(ref chunk, Player);
            Diamond_Square(size);
            chunk.Constructed = true;
        }
        Create_Mesh();
        MeshCollider mesh_col = this.gameObject.AddComponent<MeshCollider>();

    }

    private void Initialize_Chunk()
    {
        for (int i = 0; i < Not_calculated_vecs.Length; i++)
        {
            Not_calculated_vecs[i] = 1;
        }
        chunk = this;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        Initialize_Vectors();
        Chunks_Controller.Setting_Up_Coordinats(ref Coord_x, ref Coord_z,
            gameObject.transform); //!!!!!!!Need class Having_Coordinates and overload
        Chunk_name = "chunk_" + Coord_x + ";" + Coord_z;
    }

    private void Create_Mesh()
    {
        CreateTriangles();
        CreateShape();
        UpdateMesh();

      
    }

    private void CreateTriangles()
    {
        //create triangles from dots

        #region upper_tr

        //generating triangles from dots (x, x+5, x+1) last dot = (dots.len - 1) - (size + 1)
        int index = 0;
        for (int x = 0; x < Vecs.Length - 2 - size; x++)
        {
            if (((x + 1) % (size + 1) == 0) && (x != 0))
                continue;

            vertics[index] = Vecs[x];
            vertics[index + 1] = Vecs[x + (size + 1)];
            vertics[index + 2] = Vecs[x + 1];
            index += 3;
        }

        #endregion

        #region lower_tr

        //generating triangles from dots (x, x+4, x+5) last dot = dots.len - (size + 1)
        for (int x = 0; x < Vecs.Length - 1 - size; x++)
        {
            if (x % (size + 1) == 0)
                continue;

            vertics[index] = Vecs[x];
            vertics[index + 1] = Vecs[x + size];
            vertics[index + 2] = Vecs[x + (size + 1)];
            index += 3;
        }

        #endregion

        #region initializing_ordinal_numbers_massive

        //massive for creating triangles massive.len = "triangles amounts" * 3
        for (int h = 0; h < dots.Length; h++)
        {
            dots[h] = h;
        }

        #endregion
    }

    private void CreateShape()
    {
        //setting up dots and massive of ordinal numbers
        vertices = vertics;
        triangles = dots;
    }

    private void UpdateMesh()
    {
        //setting up verticles and triangles with some fixing
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    private void Diamond_Square(int mesh_size, int coord_offset = 0)
    {
        //if simple - take 2 dots height and interpolate height using dots between them with random offset

        //so if size == 1 => end function
        if (mesh_size == 1) return;

        int _size = mesh_size + 1;
        //size in function / start_size
        float multiply = mesh_size / size;

        //corner dots index
        int left_down_corner_index = 0 + coord_offset;
        int right_down_corner_index = mesh_size + coord_offset;
        int left_up_corner_index = (size + 1) * mesh_size + coord_offset;
        int right_up_corner_index = (size + 2) * mesh_size + coord_offset;
        int middle_index = (left_down_corner_index + right_up_corner_index) / 2;

        //directional dots index
        int up_index = (left_up_corner_index + right_up_corner_index) / 2;
        int left_index = (left_down_corner_index + left_up_corner_index) / 2;
        int right_index = (right_down_corner_index + right_up_corner_index) / 2;
        int down_index = (left_down_corner_index + right_down_corner_index) / 2;

        //if the first function calling
        if (mesh_size == size)
        {
            //then initialize corner dots
            Vecs[0].y += (Random.Range(-range, range) + Random.Range(-smaller_range, smaller_range) * multiply) *
                         Not_calculated_vecs[0];
            Not_calculated_vecs[0] = 0;

            Vecs[right_down_corner_index].y +=
                (Random.Range(-range, range) + Random.Range(-smaller_range, smaller_range) * multiply) *
                Not_calculated_vecs[right_down_corner_index];
            Not_calculated_vecs[right_down_corner_index] = 0;

            Vecs[left_up_corner_index].y +=
                (Random.Range(-range, range) + +Random.Range(-smaller_range, smaller_range) * multiply) *
                Not_calculated_vecs[left_up_corner_index];
            Not_calculated_vecs[left_up_corner_index] = 0;

            Vecs[right_up_corner_index].y +=
                (Random.Range(-range, range) + Random.Range(-smaller_range, smaller_range) * multiply) *
                Not_calculated_vecs[right_up_corner_index];
            Not_calculated_vecs[right_up_corner_index] = 0;
        }

        //initialize middle and directional dots

        Vecs[middle_index].y += ((Vecs[left_down_corner_index].y +
                                  Vecs[right_down_corner_index].y +
                                  Vecs[left_up_corner_index].y +
                                  Vecs[right_up_corner_index].y) / 4 +
                                 Random.Range(-smaller_range, smaller_range) * multiply) *
                                Not_calculated_vecs[middle_index];
        Not_calculated_vecs[middle_index] = 0;

        Vecs[up_index].y +=
            ((Vecs[left_up_corner_index].y + Vecs[middle_index].y + Vecs[right_up_corner_index].y) / 3 +
             Random.Range(-smaller_range, smaller_range) * multiply) * Not_calculated_vecs[up_index];
        Not_calculated_vecs[up_index] = 0;

        Vecs[left_index].y +=
            ((Vecs[left_up_corner_index].y + Vecs[middle_index].y + Vecs[left_down_corner_index].y) / 3 +
             Random.Range(-smaller_range, smaller_range) * multiply) * Not_calculated_vecs[left_index];
        Not_calculated_vecs[left_index] = 0;

        Vecs[right_index].y +=
            ((Vecs[right_up_corner_index].y + Vecs[middle_index].y + Vecs[right_down_corner_index].y) / 3 +
             Random.Range(-smaller_range, smaller_range) * multiply) * Not_calculated_vecs[right_index];
        Not_calculated_vecs[right_index] = 0;

        Vecs[down_index].y +=
            ((Vecs[left_down_corner_index].y + Vecs[middle_index].y + Vecs[right_down_corner_index].y) / 3 +
             Random.Range(-smaller_range, smaller_range) * multiply) * Not_calculated_vecs[down_index];
        Not_calculated_vecs[down_index] = 0;

        Diamond_Square(mesh_size / 2, coord_offset);
        Diamond_Square(mesh_size / 2, (left_down_corner_index + right_down_corner_index) / 2);
        Diamond_Square(mesh_size / 2, (left_down_corner_index + left_up_corner_index) / 2);
        Diamond_Square(mesh_size / 2, (left_down_corner_index + right_up_corner_index) / 2);
    }

    private void Initialize_Vectors()
    {
        for (int i = 0; i <= size; i++)
        {
            for (int j = 0; j <= size; j++)
            {
                Vecs[(i * (size + 1)) + j] =
                    new Vector3(j * size_normaller, 0, i * size_normaller); //coord for ever dot
            }
        }
    }

   
}