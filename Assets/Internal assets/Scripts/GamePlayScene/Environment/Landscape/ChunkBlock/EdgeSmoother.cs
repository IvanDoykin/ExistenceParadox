using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(ChunksBlockData))]
public class EdgeSmoother : MonoBehaviour
{
    private enum CornerDots
    {
        first,
        second,
        third,
        fourth
    }

    private EventsCollection ChunkCreated;
    ChunksBlockData chunksBlockData;

    private void Start()
    { 
        chunksBlockData = GetComponent<ChunksBlockData>();
    }

    public void Smooth(int x, int z)
    {
        DetermineDirection(x, z);
        SmoothCornerDots(x, z);
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

        if (right && left && up && down)
            ChunksBlockData.chunks[x, z].fullUpdated = true;
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

    private void SmoothCornerDots(int x, int z)
    {
        LinkCornerDots(x, z, CornerDots.first, Directions.Left, Directions.LeftDown, Directions.Down);
        LinkCornerDots(x, z, CornerDots.second, Directions.Right, Directions.Down, Directions.RightDown);
        LinkCornerDots(x, z, CornerDots.third, Directions.LeftUp, Directions.Left, Directions.Up);
        LinkCornerDots(x, z, CornerDots.fourth, Directions.Up, Directions.Right, Directions.RightUp);
    }

    private void LinkCornerDots(int x, int z, CornerDots cornerDot, params Directions[] chunksDirections)
    {
        int firstDot = 0;
        int secondDot = ChunkData.size;
        int thirdDot = ChunkData.size * (ChunkData.size + 1);
        int fourthDot = (ChunkData.size + 1) * (ChunkData.size + 1) - 1;

        int offsetX = 0;
        int offsetZ = 0;

        int parentDot = 0;
        int childDot = 0;

        switch (cornerDot)
        {
            case (CornerDots.first):
                parentDot = firstDot;
                break;

            case (CornerDots.second):
                parentDot = secondDot;
                break;

            case (CornerDots.third):
                parentDot = thirdDot;
                break;

            case (CornerDots.fourth):
                parentDot = fourthDot;
                break;
        }

        foreach (Directions directions in chunksDirections)
        {
            switch (directions)
            {
                case (Directions.Down):
                    offsetX = 0;
                    offsetZ = -1;
                    childDot = parentDot + (ChunkData.size * (ChunkData.size + 1));
                    break;

                case (Directions.Up):
                    offsetX = 0;
                    offsetZ = 1;
                    childDot = parentDot - (ChunkData.size * (ChunkData.size + 1));
                    break;

                case (Directions.Left):
                    offsetX = -1;
                    offsetZ = 0;
                    childDot = parentDot + ChunkData.size;
                    break;

                case (Directions.Right):
                    offsetX = 1;
                    offsetZ = 0;
                    childDot = parentDot - ChunkData.size;
                    break;

                case (Directions.LeftDown):
                    offsetX = -1;
                    offsetZ = -1;
                    childDot = fourthDot;
                    break;

                case (Directions.RightDown):
                    offsetX = 1;
                    offsetZ = -1;
                    childDot = thirdDot;
                    break;

                case (Directions.LeftUp):
                    offsetX = -1;
                    offsetZ = 1;
                    childDot = secondDot;
                    break;

                case (Directions.RightUp):
                    offsetX = 1;
                    offsetZ = 1;
                    childDot = firstDot;
                    break;
            }

            if (((x + offsetX) >= 0) && ((x + offsetX) < ChunksBlockData.chunksBlockSize) &&
                    ((z + offsetZ) >= 0) && ((z + offsetZ) < ChunksBlockData.chunksBlockSize))
                ChunksBlockData.chunks[x + offsetX, z + offsetZ].dots[childDot].y = ChunksBlockData.chunks[x, z].dots[parentDot].y;
        }
    }
}
