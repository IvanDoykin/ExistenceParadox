using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public sealed class SaveChunk : SaveBase<ChunkData>, ISaver
{
    private string chunksFolder;

    private void Start()
    {
        chunksFolder = Application.persistentDataPath + "/Cached/Chunks/";

        if (!Directory.Exists(chunksFolder))
        {
            DirectoryInfo directory = Directory.CreateDirectory(chunksFolder);
        }
    }

    public override void WriteData(ChunkData chunk, string chunkName)
    {
        if ((chunk == null) || (savingChunks.TryGetValue(chunkName, out SavedChunk value)))
            return;

        savingChunks.Add(chunkName, new SavedChunk(chunk.rangeOffset, chunk.fullUpdated, ConvertVectorIntoVec(chunk.dots)));
    }

    public override void SaveAll()
    {
        chunksFolder = Application.persistentDataPath + "/Cached/Chunks/";

        foreach (KeyValuePair<string, SavedChunk> chunk in savingChunks)
        {
            SaveData(chunk.Value, chunk.Key);
        }

        savingChunks.Clear();
    }

    private void SaveData(SavedChunk chunk, string chunkName)
    { 
        string path = chunksFolder + chunkName + DataFormat;

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using(Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Delete))
        {
            binaryFormatter.Serialize(fStream, chunk);
        }
    }

    private Vec3[] ConvertVectorIntoVec(Vector3[] vectorMassive)
    {
        Vec3[] convertedVec = new Vec3[vectorMassive.Length];

        for(int i = 0; i < convertedVec.Length; i++)
        {
            convertedVec[i] = ConvertVectorIntoVec(vectorMassive[i]);
        }

        return convertedVec;
    }

    private Vec3 ConvertVectorIntoVec(Vector3 vector)
    {
        return new Vec3(vector.x, vector.y, vector.z);
    }

    public void OnDestroy()
    {
        Save();
    }

    public void Save()
    {
        WriteData(GetComponent<ChunkData>(), GetComponent<ChunkNameData>().value);
    }
}
