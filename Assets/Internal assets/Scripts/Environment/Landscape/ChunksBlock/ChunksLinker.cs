using UnityEngine;

public class ChunksLinker : MonoBehaviour
{
    public static event ChunkGenerator.CallChunkLinking NeedLink;

    //calculating chunk's relative coordinates and link that with others chunks
    public void LinkChunk(CoordinatingData coordinatesData)
    {
        int x = coordinatesData.x - ChunksBlockData.zeroPointX;
        int z = coordinatesData.z - ChunksBlockData.zeroPointZ;

        LinkChunk(x, z);
    }

    //link chunk with neighbour ones
    private void LinkChunk(int x, int z)
    {
        bool right = x != ChunksBlockData.halfChunkBlockSize;
        bool left = x != -ChunksBlockData.halfChunkBlockSize;
        bool up = z != ChunksBlockData.halfChunkBlockSize;
        bool down = z != -ChunksBlockData.halfChunkBlockSize;

        if (right)
            EqualEdgeDots(x, z, Directions.Right);

        if (left)
            EqualEdgeDots(x, z, Directions.Left);

        if (down)
            EqualEdgeDots(x, z, Directions.Down);

        if (up)
            EqualEdgeDots(x, z, Directions.Up);

        if (right && down)
            EqualEdgeDots(x, z, Directions.RightDown);

        if (right && up)
            EqualEdgeDots(x, z, Directions.RightUp);

        if (left && down)
            EqualEdgeDots(x, z, Directions.LeftDown);

        if (left && up)
            EqualEdgeDots(x, z, Directions.LeftUp);
    }

    //find edge dots and equal their Y-coordinates
    private void EqualEdgeDots(int x, int z, Directions direction)
    {
        int indexAdditionX = 0;
        int indexAdditionZ = 0;
        int startPoint = 0;
        int step = 0;
        int otherChunkDot = 0;
        int offsetDown = 0;

        switch (direction)
        {
            case Directions.Right:
                {
                    indexAdditionX = 1;
                    indexAdditionZ = 0;
                    startPoint = ChunkData.Size;
                    step = ChunkData.Size + 1;
                    otherChunkDot = -ChunkData.Size;
                    break;
                }

            case Directions.Left:
                {
                    indexAdditionX = -1;
                    indexAdditionZ = 0;
                    startPoint = 0;
                    step = ChunkData.Size + 1;
                    otherChunkDot = ChunkData.Size;
                    break;
                }

            case Directions.Up:
                {
                    indexAdditionX = 0;
                    indexAdditionZ = 1;
                    startPoint = ChunkData.Size * (ChunkData.Size + 1);
                    step = 1;
                    otherChunkDot = -startPoint;
                    break;
                }

            case Directions.Down:
                {
                    indexAdditionX = 0;
                    indexAdditionZ = -1;
                    startPoint = 0;
                    step = 1;
                    otherChunkDot = ChunkData.Size * (ChunkData.Size + 1);
                    offsetDown = -otherChunkDot;
                    break;
                }

            case Directions.RightUp:
                {
                    indexAdditionX = 1;
                    indexAdditionZ = 1;
                    startPoint = (ChunkData.Size + 1) * (ChunkData.Size + 1) - 1;
                    step = startPoint + 1;
                    otherChunkDot = -startPoint;
                    break;
                }

            case Directions.LeftUp:
                {
                    indexAdditionX = -1;
                    indexAdditionZ = 1;
                    startPoint = ChunkData.Size * (ChunkData.Size + 1);
                    step = startPoint + ChunkData.Size + 1;
                    otherChunkDot = ChunkData.Size - startPoint;
                    break;
                }

            case Directions.RightDown:
                {
                    indexAdditionX = 1;
                    indexAdditionZ = -1;
                    startPoint = ChunkData.Size;
                    step = (ChunkData.Size + 1) * (ChunkData.Size + 1) + 1;
                    otherChunkDot = ChunkData.Size * ChunkData.Size;
                    break;
                }

            case Directions.LeftDown:
                {
                    indexAdditionX = -1;
                    indexAdditionZ = -1;
                    startPoint = 0;
                    step = (ChunkData.Size + 1) * (ChunkData.Size + 1) + 1;
                    otherChunkDot = step - 2;
                    offsetDown = -ChunkData.Size * (ChunkData.Size + 1);
                    break;
                }
        }

        ChunkData parentChunk = ChunksBlockData.chunks[x + indexAdditionX + ChunksBlockData.halfChunkBlockSize, z + indexAdditionZ + ChunksBlockData.halfChunkBlockSize];
        ChunkData childChunk = ChunksBlockData.chunks[x + ChunksBlockData.halfChunkBlockSize, z + ChunksBlockData.halfChunkBlockSize];

        if ((parentChunk != null) && (parentChunk.constructed == true))
        {
            int dotsLength = (ChunkData.Size + 1) * (ChunkData.Size + 1);

            for (int i = startPoint; i < dotsLength + offsetDown; i += step)
            {
                childChunk.dots[i].y = parentChunk.dots[i + otherChunkDot].y;

                childChunk.notCalculatedVecs[i] = 0;
            }
        }
    }

}
