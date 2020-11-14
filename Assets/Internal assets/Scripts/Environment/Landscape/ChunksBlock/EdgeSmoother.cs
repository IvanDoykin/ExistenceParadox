using UnityEngine;

[RequireComponent(typeof(ChunksBlockData))]

public class EdgeSmoother : MonoBehaviour, IEventTrigger
{
    //for convinient describing corner dots in chunks
    private enum CornerDots
    {
        first,
        second,
        third,
        fourth
    }

    //..maybe legacy :/
    [SerializeField] private EventsCollection ChunkCreated;

    //synchronize corner dots with neighbour chunks
    public void Smooth(int x, int z)
    {
        DetermineDirection(x, z);
        SmoothCornerDots(x, z);
        TriggerEvent(ChunkCreated.currentEvent);
    }

    //Determine, what directions we can use with current chunk (or we get OutOfRange Exception)
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

    //Make chunk more smooth, without sharp edges
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
                    startPoint = ChunkData.Size;
                    step = ChunkData.Size + 1;
                    otherChunkDot = -ChunkData.Size;
                    chunkDotOffset = 1;
                    break;
                }

            case Directions.Left:
                {
                    indexAdditionX = -1;
                    indexAdditionZ = 0;
                    startPoint = 0;
                    step = ChunkData.Size + 1;
                    otherChunkDot = ChunkData.Size;
                    chunkDotOffset = -1;
                    break;
                }

            case Directions.Up:
                {
                    indexAdditionX = 0;
                    indexAdditionZ = 1;
                    startPoint = ChunkData.Size * (ChunkData.Size + 1);
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
                    otherChunkDot = ChunkData.Size * (ChunkData.Size + 1);
                    offsetDown = -otherChunkDot;
                    chunkDotOffset = -9;
                    break;
                }
        }

        ChunkData parentChunk = ChunksBlockData.chunks[x + indexAdditionX, z + indexAdditionZ];
        ChunkData childChunk = ChunksBlockData.chunks[x, z];

        if ((parentChunk != null) && (parentChunk.constructed == true))
        {
            int dotsLength = (ChunkData.Size + 1) * (ChunkData.Size + 1);

            for (int i = startPoint; i < dotsLength + offsetDown; i += step)
            {
                childChunk.dots[i].y = MeshCreator.Average(childChunk.dots[i - chunkDotOffset].y, parentChunk.dots[i + otherChunkDot + chunkDotOffset].y);
                parentChunk.dots[i + otherChunkDot].y = childChunk.dots[i].y;
            }
        }
    }

    //Link chunks in their corner dots
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
        int secondDot = ChunkData.Size;
        int thirdDot = ChunkData.Size * (ChunkData.Size + 1);
        int fourthDot = (ChunkData.Size + 1) * (ChunkData.Size + 1) - 1;

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
                    childDot = parentDot + (ChunkData.Size * (ChunkData.Size + 1));
                    break;

                case (Directions.Up):
                    offsetX = 0;
                    offsetZ = 1;
                    childDot = parentDot - (ChunkData.Size * (ChunkData.Size + 1));
                    break;

                case (Directions.Left):
                    offsetX = -1;
                    offsetZ = 0;
                    childDot = parentDot + ChunkData.Size;
                    break;

                case (Directions.Right):
                    offsetX = 1;
                    offsetZ = 0;
                    childDot = parentDot - ChunkData.Size;
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

    //..maybe legacy :/
    public void TriggerEvent(string eventName, params object[] arguments)
    {
        ManagerEvents.CheckTriggeringEvent(eventName, arguments);
    }
}
