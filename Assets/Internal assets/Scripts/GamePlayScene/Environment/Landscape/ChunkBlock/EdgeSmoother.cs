using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChunksBlockData))]
public class EdgeSmoother : MonoBehaviour
{
    private EventsCollection ChunkCreated;
    ChunksBlockData chunksBlockData;

    private void Start()
    { 
        chunksBlockData = GetComponent<ChunksBlockData>();
    }

    public void Smooth(int x, int z)
    {
        DetermineDirection(x, z);

        //ChunksBlockData.chunks[x, z].gameObject.GetComponent<MeshFilter>().mesh.Clear();

        //CreateMesh();
        //Destroy(GetComponent<MeshCollider>());
        //MeshCollider mesh_col = this.gameObject.AddComponent<MeshCollider>();
    }

    private void DetermineDirection(int x, int z)
    {
        bool right = (x - ChunksBlockData.halfChunkBlockSize) != ChunksBlockData.halfChunkBlockSize;
        bool left = (x - ChunksBlockData.halfChunkBlockSize) != -ChunksBlockData.halfChunkBlockSize;
        bool up = (z - ChunksBlockData.halfChunkBlockSize) != ChunksBlockData.halfChunkBlockSize;
        bool down = (z - ChunksBlockData.halfChunkBlockSize) != -ChunksBlockData.halfChunkBlockSize;

        if (right)
            SmoothEdge(x, z, Directions.Right);

        if (left)
            SmoothEdge(x, z, Directions.Left);

        if (down)
            SmoothEdge(x, z, Directions.Down);

        if (up)
            SmoothEdge(x, z, Directions.Up);
    }

    private void SmoothEdge(int x, int z, Directions direction)
    {
        int indexAdditionX = 0;
        int indexAdditionZ = 0;
        int startPoint = 0;
        int step = 0;
        int otherChunkDot = 0;
        int offsetDown = 0;
        int chunkDotOffset = 0;

        switch (direction)
        {
            case Directions.Right:
                {
                    indexAdditionX = 1;
                    indexAdditionZ = 0;
                    startPoint = ChunkData.size;
                    step = ChunkData.size + 1;
                    otherChunkDot = -ChunkData.size;
                    chunkDotOffset = 1;
                    break;
                }

            case Directions.Left:
                {
                    indexAdditionX = -1;
                    indexAdditionZ = 0;
                    startPoint = 0;
                    step = ChunkData.size + 1;
                    otherChunkDot = ChunkData.size;
                    chunkDotOffset = -1;
                    break;
                }

            case Directions.Up:
                {
                    indexAdditionX = 0;
                    indexAdditionZ = 1;
                    startPoint = ChunkData.size * (ChunkData.size + 1);
                    step = 1;
                    otherChunkDot = -startPoint;
                    chunkDotOffset = 9;
                    break;
                }

            case Directions.Down:
                {
                    indexAdditionX = 0;
                    indexAdditionZ = -1;
                    startPoint = 0;
                    step = 1;
                    otherChunkDot = ChunkData.size * (ChunkData.size + 1);
                    offsetDown = -otherChunkDot;
                    chunkDotOffset = -9;
                    break;
                }
        }

        Debug.Log("direction = " + direction);
        Debug.Log("parent_chunk = [" + (x + indexAdditionX) + "; " + (z + indexAdditionZ) + "]");
        Debug.Log("child_chunk = [" + x + "; " + z + "]");

        ChunkData parentChunk = ChunksBlockData.chunks[x + indexAdditionX, z + indexAdditionZ];
        ChunkData childChunk = ChunksBlockData.chunks[x, z];

        if ((parentChunk != null) && (parentChunk.constructed == true))
        {
            int dotsLength = (ChunkData.size + 1) * (ChunkData.size + 1);

            for (int i = startPoint; i < dotsLength + offsetDown; i += step)
            {
                childChunk.dots[i].y = SecondaryChunkGenerating.Average(childChunk.dots[i - chunkDotOffset].y, parentChunk.dots[i + otherChunkDot + chunkDotOffset].y);
                parentChunk.dots[i + otherChunkDot].y = childChunk.dots[i].y;
            }
        }
    }

    private void SmoothCornerDots()
    {

    }
}
