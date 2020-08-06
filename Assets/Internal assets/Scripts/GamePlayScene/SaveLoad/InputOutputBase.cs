using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class InputOutputBase : MonoBehaviour
{
    protected static Dictionary<string, SavedChunk> savingChunks = new Dictionary<string, SavedChunk>();

    protected const string DataFormat = ".dat";

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

    [Serializable]
    protected struct SavedChunk
    {
        public Vec3 position;
        public float rangeOffset;
        public bool fullUpdated;

        public Vec3[] dots;

        public SavedChunk(Vec3 Position, float RangeOffset, bool FullUpdated, Vec3[] Dots)
        {
            position = Position;
            rangeOffset = RangeOffset;
            fullUpdated = FullUpdated;
            dots = Dots;
        }
    }
}
