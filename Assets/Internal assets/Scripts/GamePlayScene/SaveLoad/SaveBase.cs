using UnityEngine;
using System;

public abstract class SaveBase<SavingData> : MonoBehaviour where SavingData : Saveable
{
    protected const string DataFormat = ".dat";

    public abstract void Save();
    public abstract void WriteData(SavingData someData, string name);

    [Serializable]
    public struct Vec3
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
}
