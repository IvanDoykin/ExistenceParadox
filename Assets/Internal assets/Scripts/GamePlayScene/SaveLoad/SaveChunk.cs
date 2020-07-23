using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public sealed class SaveChunk : SaveBase<ChunkData>
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
        if (chunk == null)
        {
            Debug.LogError("WriteChunkData ERROR: Chunk is 'null'");
            return;
        }

        if (savingChunks.TryGetValue(chunkName, out ChunkData value)) return;

        savingChunks.Add(chunkName, chunk);
    }

    public override void SaveAll()
    {
        chunksFolder = Application.persistentDataPath + "/Cached/Chunks/";

        foreach (KeyValuePair<string, ChunkData> chunk in savingChunks)
        {
            SaveData(chunk.Value, chunk.Key);
        }

        savingChunks.Clear();
    }

    private void SaveData(ChunkData chunk, string chunkName)
    { 
        SavedChunk savedChunk = new SavedChunk(ConvertVectorIntoVec(chunk.position), chunk.rangeOffset, chunk.fullUpdated, ConvertVectorIntoVec(chunk.dots));
        string path = chunksFolder + chunkName + DataFormat;

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using(Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Delete))
        {
            binaryFormatter.Serialize(fStream, savedChunk);
            Debug.Log("Its fine");
            Debug.Log("Path: " + path);
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

}
