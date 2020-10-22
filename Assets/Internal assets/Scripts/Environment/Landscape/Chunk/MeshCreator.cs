using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator : MonoBehaviour
{
    public static float Average(params float[] numbers)
    {
        float sum = 0;
        foreach (float f in numbers)
        {
            sum += f;
        }

        return (sum / numbers.Length);
    }

    //it does some realtively low-level things for creating mesh
    public void CreateMesh(ref ChunkData chunk)
    {
        CreateTriangles(ref chunk);
        CreateShape(ref chunk);
        UpdateMesh(ref chunk);

        //MeshCollider meshCollider = this.gameObject.AddComponent<MeshCollider>();
        //meshCollider.sharedMesh = chunk.mesh;
    }

    private void CreateTriangles(ref ChunkData chunk)
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

    private void CreateShape(ref ChunkData chunk)
    {
        //setting up dots and massive of ordinal numbers
        chunk.generatedVertices = chunk.vertices;
        chunk.triangles = chunk.ordinalNumbers;
    }

    private void UpdateMesh(ref ChunkData chunk)
    {
        Mesh updatebleMesh = gameObject.GetComponent<MeshFilter>().mesh;

        //setting up verticles and triangles with some fixing
        updatebleMesh.Clear();

        updatebleMesh.vertices = chunk.generatedVertices;
        updatebleMesh.triangles = chunk.triangles;

        updatebleMesh.RecalculateNormals();
        updatebleMesh.RecalculateBounds();

        updatebleMesh = chunk.mesh;
    }

}
