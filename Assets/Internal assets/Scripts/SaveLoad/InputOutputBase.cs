using UnityEngine;
using System;
using System.Collections.Generic;

//That class keep cache-dictionaries, some general info and need structures for convert [monobehaviour data <-> non-monobehaviour data]
public abstract class InputOutputBase : MonoBehaviour
{
    protected static Dictionary<string, SavedChunk> savingChunks = new Dictionary<string, SavedChunk>();

    protected const string DataFormat = ".dat";

    //Vector <-> Vec3
    [Serializable]
    protected struct Vec3
    {
        public float x;
        public float y;
        public float z;

        public Vec3(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
        }
    }

    //ChunkData <-> SavedChunk
    [Serializable]
    protected struct SavedChunk
    {
        public float rangeOffset;
        public bool fullUpdated;

        public Vec3[] dots;

        public SavedChunk(float RangeOffset, bool FullUpdated, Vec3[] Dots)
        {
            rangeOffset = RangeOffset;
            fullUpdated = FullUpdated;
            dots = Dots;
        }
    }
}
